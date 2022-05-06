using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.XR;

public class VrHandsController : MonoBehaviour
{

    /*
    When I click the grab button I want to check for any HoldingAnchors in a certain distance from the hand, if there are any I want them to be put in my hand as long as I keep my hand closed
    When I click the trigger while I have a holdable object in my hand check for if it is an activatable then it should activate the Fireweapon script in the weaponcontroller
    */

    public enum Hand
    {

        Right,
        Left

    }

    public Hand HandLR;
    public VRPlayerInputs PlayerInputs;
    public Transform DisplayTransform;
    public float HapticAmp, HapticDur;
    public bool HandInteractionEnabled;
    public LineRenderer LaserPointer;
    [SerializeField]
    private float grabRadius, grabDistance;
    private LayerMask grabMask;
    [SerializeField]
    private HoldingAnchor holding, closestAnchor;
    private InputAction GrabButton, TriggerButton;
    private PlayerItemController itemController;
    private UnityEngine.XR.InputDevice device;
    private UnityEngine.XR.HapticCapabilities deviceHaptics;

    private void Awake()
    {

        PlayerInputs = new VRPlayerInputs();

        switch(HandLR)
        {

            case Hand.Right:
            GrabButton = PlayerInputs.Player.GrabRight;
            TriggerButton = PlayerInputs.Player.FireRight;

            break;

            case Hand.Left:
            GrabButton = PlayerInputs.Player.GrabLeft;
            TriggerButton = PlayerInputs.Player.FireLeft;

            break;

        }

    }

    private void OnEnable()
    {

        GrabButton.Enable();
        TriggerButton.Enable();

    }

    private void OnDisable()
    {

        GrabButton.Disable();
        TriggerButton.Disable();

    }

    private void Start()
    {

        grabMask = 1 << LayerMask.NameToLayer("Grabbable");
        itemController = transform.root.GetComponent<PlayerItemController>();
        HandInteractionEnabled = true;

        switch(HandLR)
        {

            case Hand.Right:

            device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            device.TryGetHapticCapabilities(out deviceHaptics);

            break;

            case Hand.Left:

            device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            device.TryGetHapticCapabilities(out deviceHaptics);

            break;

        }
        
    }

    public void SetInteractionState(bool _on)
    {

        HandInteractionEnabled = _on;

        if(!_on)
        {

            if(holding)
                holding.Released();
            holding = null;

        }

    }

    private void Update()
    {

        if(HandInteractionEnabled)
        {

            if(!holding)
            {

                Collider[] _hits = Physics.OverlapSphere(transform.position, grabRadius, grabMask, QueryTriggerInteraction.Collide);

                if(_hits.Length > 0)
                {

                    Collider _closestHit = _hits[0];
                    float[] _distances = new float[_hits.Length];

                    for (int i = 0; i < _hits.Length; i++)
                    {

                        _distances[i] = Vector3.Distance(_hits[i].transform.position, transform.position);

                        if(_distances[i] == _distances.Min())
                        {

                            _closestHit = _hits[i];

                        }
                        
                    }

                    _closestHit.transform.TryGetComponent<HoldingAnchor>(out closestAnchor);

                }
                else
                {

                    closestAnchor = null;

                }

                if(!closestAnchor)
                {

                    if(Physics.Raycast(transform.position, transform.forward, out RaycastHit _hit, grabDistance, grabMask, QueryTriggerInteraction.Collide))
                    {

                        _hit.transform.TryGetComponent<HoldingAnchor>(out closestAnchor);

                        LaserPointer.SetPosition(0, transform.position);
                        LaserPointer.SetPosition(1, _hit.point);

                    }
                    else
                    {

                        LaserPointer.SetPosition(0, transform.position);
                        LaserPointer.SetPosition(1, transform.position);
                        
                    }

                }
                else
                {

                    LaserPointer.SetPosition(0, transform.position);
                    LaserPointer.SetPosition(1, transform.position);

                }
                
                if(closestAnchor)
                {

                    if(!closestAnchor.IsHeld)
                    {

                        if(deviceHaptics.supportsImpulse)
                        {
                            
                            device.SendHapticImpulse(0u, HapticAmp, HapticDur);
                        
                        }
                        else if(deviceHaptics.supportsBuffer)
                        {

                            Debug.Log("Buffer supported, but not implemented...");

                        }
                        else
                            Debug.Log("No Haptics");

                    }
                    
                }

            }

            if(GrabButton.WasPressedThisFrame() && closestAnchor)
            {

                if(!closestAnchor.IsHeld)
                {

                    holding = closestAnchor.Grabbed(transform);

                }
                else
                {

                    holding = null;

                }

            }

            if(GrabButton.WasReleasedThisFrame() && holding)
            {

                holding.Released();
                holding = null;

            }

            if(TriggerButton.WasPressedThisFrame())
            {
                
                if(holding)
                {

                    if(holding.TryGetComponent<HoldingAnchorActivatable>(out HoldingAnchorActivatable _activatable))
                    {
                        
                        _activatable.ActivateInteractableOnObject(transform, true);

                    }

                }
                else if(itemController.PlacingTrap)
                {

                    itemController.StartPlacingTrap();

                }

            }
            else if(TriggerButton.WasReleasedThisFrame() && holding)
            {

                if(holding.TryGetComponent<HoldingAnchorActivatable>(out HoldingAnchorActivatable _activatable))
                {

                    _activatable.ActivateInteractableOnObject(transform, false);

                }

            }

        }


    }
    
}