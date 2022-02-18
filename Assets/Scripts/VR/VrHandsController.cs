using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class VRHandsController : MonoBehaviour
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

    public VRPlayerInputs PlayerInputs;
    public GameObject RightHand, LeftHand;
    public Transform WeaponDisplay, TrapDisplay;
    [SerializeField]
    private float grabRadius, grabMaxDist, grabDistance;
    private LayerMask grabMask;
    private HoldingAnchor holding;
    private InputAction GrabButton, TriggerButton;

    private void Awake()
    {

        PlayerInputs = new VRPlayerInputs();

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

        grabMask = ~(1 << LayerMask.NameToLayer("Player"));
        
    }

    private void Update()
    {

        if(GrabButton.WasPressedThisFrame())
        {

            RaycastHit[] _hits = Physics.SphereCastAll(transform.position, grabRadius, Vector3.forward, grabMaxDist, grabMask, QueryTriggerInteraction.Ignore);

            if(_hits.Length > 0)
            {

                RaycastHit _closestHit = _hits[0];
                float[] _distances = new float[_hits.Length];
                float _closest = 0;

                for (int i = 0; i < _hits.Length; i++)
                {

                    _distances[i] = Vector3.Distance(_hits[i].point, transform.position);
                    if(_distances[i] == _distances.Min())
                    {

                        _closestHit = _hits[i];
                        _closest = _distances.Min();

                    }
                    
                }

                if(_closest <= grabDistance)
                {

                    holding = _closestHit.transform.GetComponent<HoldingAnchor>();
                    holding.Grabbed(transform);

                }

            }

        }

        if(GrabButton.WasReleasedThisFrame())
        {

            holding.Released();

        }

    }
    
}