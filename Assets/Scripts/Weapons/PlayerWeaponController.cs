using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerWeaponController : MonoBehaviourPunCallbacks
{
    //Plans/Info
    /*

    Traps
    id (has to be same as weapon)
    Scaling mode
    Max and Min scale
    Prefab of trap
    Bounds, use a collider on the Root object for the trap to set its bounds so it scales correctly

    Weapon
    id (Same as trap)
    WeaponUseMode
    MeleeRach
    ThrowVelocity
    ShotRange

    TODO
    - Align placement with surface
    - Exessive placement checks for checking all directions to keep traps out of walls
    - Have a "Weapon editor" where you can change settings for individual weapons

    */

    public enum ScalingMode
    {

        None,
        Forward,
        Sideways,
        SurfaceAlign

    }

    public enum WeaponUseMode
    {

        Melee,
        Throw,
        Ranged

    }

    [Tooltip("This might be a bit heavy")]
    public bool ExstensivePlacementChecks, UseUIFeedback;
    public GameObject[] TrapPrefabs, WeaponPrefabs;
    public ScalingMode[] TrapScaleMode;
    public float[] TrapActivationDelay;
    public WeaponUseMode[] AttackMode;
    public float TrapPlaceDistance, PlacementCheckRange, ScaleMaxLenght, ScaleMinLenght, MeleeReach, ThrowVelocity, ShotRange;
    public int EquippedWeaponID {get; private set;}
    public Transform Parent;
    [SerializeField]
    private GameObject droppedWeaponPrefab;
    [SerializeField]
    private Text weaponIDText, feedbackText;
    private bool placingTrap, validLocation;
    private float timer;
    private (Vector3, Vector3, Quaternion) placementInfo;
    private Transform ghostTrap, weaponInHand;
    private Vector3 trapBounds, boundsOffset;
    private LayerMask mask, playerMask;

    private void Start()
    {

        mask = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Traps")));
        playerMask = 1 << LayerMask.NameToLayer("Player");

        if(UseUIFeedback)
        {

            feedbackText.text = "";

        }

    }

    public void PickedUpWeapon(int weaponID)
    {

        if(EquippedWeaponID != 0)
        {

            GameObject temp = PhotonNetwork.Instantiate("PickupDroppedWeaponPrefab", transform.position, Quaternion.identity);
            temp.GetComponent<WeaponPickup>().WeaponID = EquippedWeaponID;
            temp.name = "Dropped Weapon ^ " + EquippedWeaponID;

        }
        
        placingTrap = false;
        UpdateEquippedWeapon(weaponID);

    }

    private void UpdateEquippedWeapon(int iD)
    {

        EquippedWeaponID = iD;

        if(UseUIFeedback)
        {

            weaponIDText.text = "^ Weapon ID: " + EquippedWeaponID + " ^";

        }

    }

    #region Placement Modes

    private (Vector3 position, Vector3 scale, Quaternion rotation) AlignToSurface(Vector3 tempPos)
    {

        return (Vector3.zero, Vector3.one, Quaternion.identity);

    }

    private (Vector3 position, Vector3 scale, Quaternion rotation) ScaleSideways(Vector3 tempPos)
    {

        Vector3 _position, _scale;
        Quaternion _rotation;

        RaycastHit hit;
        if(Physics.Raycast(tempPos, Vector3.down, out hit, 1, mask, QueryTriggerInteraction.Ignore))
        {

            _position = hit.point + (Vector3.up * (trapBounds.y / boundsOffset.y));
            validLocation = true;

            float rightDist = 0, leftDist = 0;

            if(Physics.Raycast(_position, ghostTrap.right, out hit, ScaleMaxLenght/2, mask, QueryTriggerInteraction.Ignore))
            {

                rightDist = Vector3.Distance(_position, hit.point);

            }

            if(Physics.Raycast(_position, ghostTrap.right * -1, out hit, ScaleMaxLenght/2, mask, QueryTriggerInteraction.Ignore))
            {

                leftDist = Vector3.Distance(_position, hit.point);

            }

            if(rightDist != 0 || leftDist != 0)
            {

                if((rightDist < leftDist || leftDist < ScaleMinLenght) && rightDist > ScaleMinLenght)
                {

                    _scale = new Vector3(2 * rightDist, 1, 1);

                }
                else if((leftDist < rightDist || rightDist < ScaleMinLenght) && leftDist > ScaleMinLenght)
                {

                    _scale = new Vector3(2 * leftDist, 1, 1);

                }
                else
                {

                    //invalid?
                    _scale = Vector3.one;

                }

            }
            else
            {
                
                _scale = new Vector3(1 * ScaleMaxLenght, 1, 1);

            }

        }
        else
        {

            _position = tempPos + (Vector3.up * (trapBounds.y - boundsOffset.y));
            _scale = Vector3.one;
            validLocation = false;

        }

        _rotation = Parent.rotation;

        return (_position, _scale, _rotation);

    }

    private (Vector3 position, Vector3 scale, Quaternion rotation) ScaleForward(Vector3 tempPos)
    {

        Vector3 _position, _scale;
        Quaternion _rotation;

        RaycastHit hit;
        if(Physics.Raycast(tempPos, Vector3.down, out hit, 1, mask, QueryTriggerInteraction.Ignore))
        {

            _position = hit.point + (Vector3.up * (trapBounds.y - boundsOffset.y));
            validLocation = true;

            if(Physics.Raycast(_position, ghostTrap.forward, out hit, ScaleMaxLenght / 2, mask, QueryTriggerInteraction.Ignore))
            {

                _scale = new Vector3(1, 1, 1 * Mathf.Clamp(Vector3.Distance(_position, hit.point) * 2, ScaleMinLenght * 2, ScaleMaxLenght));

            }
            else
            {

                _scale = new Vector3(1, 1, 1 * ScaleMaxLenght);

            }

        }
        else
        {

            _position = tempPos + (Vector3.up * (trapBounds.y - boundsOffset.y));
            _scale = Vector3.one;
            validLocation = false;

        }

        _rotation = Parent.rotation;

        return (_position, _scale, _rotation);

    }

    private (Vector3 position, Vector3 scale, Quaternion rotation) NoScaling(Vector3 tempPos)
    {

        Vector3 _position;
        Quaternion _rotation;

        RaycastHit hit;
        if(Physics.Raycast(tempPos, Vector3.down, out hit, 1, mask, QueryTriggerInteraction.Ignore))
        {

            _position = hit.point + (Vector3.up * (trapBounds.y - boundsOffset.y));
            validLocation = true;

        }
        else
        {

            _position = tempPos + (Vector3.up * (trapBounds.y - boundsOffset.y));
            validLocation = false;

        }

        //might wanna change this later, maybe?
        _rotation = Parent.rotation;

        return (_position, Vector3.zero, _rotation);

    }

    #endregion

    private void EquipWeapon()
    {

        //Desktop

        weaponInHand = WeaponPrefabs[EquippedWeaponID].transform;

    }

    private void ThrowWeapon()
    {

        //Desktop

        GameObject temp = PhotonNetwork.Instantiate(WeaponPrefabs[EquippedWeaponID].name, transform.position, transform.rotation);
        WeaponController controller = temp.GetComponent<WeaponController>();
        controller.WeaponID = EquippedWeaponID;
        controller.Thrown();
        temp.GetComponent<Rigidbody>().velocity = transform.forward * ThrowVelocity;

        UpdateEquippedWeapon(0);

    }

    private void FireWeapon()
    {

        //VR and Desktop
            weaponInHand.GetComponent<WeaponController>().FireWeapon();

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Mouse1) && EquippedWeaponID != 0)
        {

            string tempFeedback = "";

            if(!placingTrap)
            {

                placingTrap = true;
                ghostTrap = Instantiate(TrapPrefabs[EquippedWeaponID]).transform;
                trapBounds = ghostTrap.GetComponent<BoxCollider>().bounds.extents;
                boundsOffset = ghostTrap.GetComponent<BoxCollider>().bounds.center;

            }
            else if(validLocation)
            {

                Transform temp = PhotonNetwork.Instantiate(TrapPrefabs[EquippedWeaponID].name, placementInfo.Item1, placementInfo.Item3).transform;
                if(TrapScaleMode[EquippedWeaponID] != ScalingMode.None)
                    temp.localScale = placementInfo.Item2;
                temp.gameObject.AddComponent<TrapController>().TrapPlaced(TrapActivationDelay[EquippedWeaponID], EquippedWeaponID);
                Destroy(ghostTrap.gameObject);
                UpdateEquippedWeapon(0);
                tempFeedback = "";

            }
            else
            {

                tempFeedback = "Placement is not valid";
                timer = 2;

            }

            if(UseUIFeedback)
            {

                feedbackText.text = tempFeedback;

            }

        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && EquippedWeaponID != 0)
        {

            switch(AttackMode[EquippedWeaponID])
            {

                case WeaponUseMode.Melee:
                //MeleeAttack();
                break;

                case WeaponUseMode.Throw:
                ThrowWeapon();
                break;

                case WeaponUseMode.Ranged:
                FireWeapon();
                break;

            }

        }

        if(Input.GetKeyDown(KeyCode.R) && EquippedWeaponID != 0)
        {

            //weaponInHand =

        }

        if(UseUIFeedback)
        {

            if(timer > 0)
            {

                timer -= Time.deltaTime;

            }
            else if(feedbackText.text != "")
            {

                feedbackText.text = "";

            }
        
        }

    }

    private void FixedUpdate()
    {

        if(placingTrap && EquippedWeaponID != 0)
        {

            Vector3 tempV3;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, TrapPlaceDistance, mask, QueryTriggerInteraction.Ignore))
            {

                tempV3 = hit.point + Vector3.up * 0.25f;

            }
            else
            {

                tempV3 = transform.position + (transform.forward * TrapPlaceDistance);

            }

            switch(TrapScaleMode[EquippedWeaponID])
            {

                case ScalingMode.None:
                placementInfo = NoScaling(tempV3);
                break;

                case ScalingMode.Forward:
                placementInfo = ScaleForward(tempV3);
                break;

                case ScalingMode.Sideways:
                placementInfo = ScaleSideways(tempV3);
                break;

            }

            ghostTrap.SetPositionAndRotation(placementInfo.Item1, placementInfo.Item3);
            if(TrapScaleMode[EquippedWeaponID] != ScalingMode.None)
                ghostTrap.localScale = placementInfo.Item2;

        }
        else
        {

            placingTrap = false;
            if(ghostTrap)
                Destroy(ghostTrap.gameObject);

        }

    }
    
}