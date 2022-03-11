using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

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
    public GameObject RightHand, LeftHand;
    public Transform WeaponDisplay, TrapDisplay;
    [SerializeField]
    private float grabRadius, grabMaxDist, grabDistance;
    private LayerMask grabMask;
    [SerializeField]
    private HoldingAnchor holding;
    private InputAction GrabButton, TriggerButton;
    private PlayerItemController itemController;

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

        grabMask = (1 << LayerMask.NameToLayer("Interactables") + 1 << LayerMask.NameToLayer("DangerInteractable"));
        itemController = transform.root.GetComponent<PlayerItemController>();
        
    }

    private void Update()
    {

        if(GrabButton.WasPressedThisFrame())
        {

            RaycastHit[] _hits = Physics.SphereCastAll(transform.position, grabRadius, Vector3.forward, grabMaxDist, grabMask, QueryTriggerInteraction.Collide);

            if(_hits.Length > 0)
            {

                RaycastHit _closestHit = _hits[0];
                float[] _distances = new float[_hits.Length];
                float _closest = 0;

                for (int i = 0; i < _hits.Length; i++)
                {

                    _distances[i] = Vector3.Distance(_hits[i].transform.position, transform.position);

                    if(_distances[i] == _distances.Min())
                    {

                        _closestHit = _hits[i];

                    }
                    
                }

                if(_closest <= grabDistance && _closestHit.transform.TryGetComponent<HoldingAnchor>(out holding))
                {

                    if(!holding.IsHeld)
                    {

                        holding = holding.Grabbed(transform);

                    }
                    else
                    {

                        holding = null;

                    }

                }

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