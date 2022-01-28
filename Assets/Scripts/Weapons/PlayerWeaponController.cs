using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
    //TODO
    /*
    - Sepperate functions for the different scaling/placement
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
    public bool ExstensivePlacementChecks;
    public GameObject[] TrapPrefabs {get; private set;}
    public ScalingMode[] TrapScaleMode {get; private set;}
    public WeaponUseMode[] AttackMode {get; private set;}
    public float TrapPlaceDistance, PlacementCheckRange, ScaleMaxLenght, ScaleMinLenght;
    public int EquippedWeaponID {get; private set;}
    [SerializeField]
    private GameObject droppedWeaponPrefab;
    [SerializeField]
    private Text weaponIDText;
    private bool placingTrap, validLocation;
    private (Vector3, Vector3, Quaternion) placementInfo;
    private Transform ghostTrap;
    private Vector3 trapBounds, boundsOffset;
    private LayerMask mask;

    private void Start()
    {

        mask = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Traps")));

    }

    public void PickedUpWeapon(int weaponID)
    {

        if(EquippedWeaponID != 0)
            Instantiate(droppedWeaponPrefab, transform.position, Quaternion.identity).GetComponent<WeaponPickup>().WeaponID = EquippedWeaponID;
        
        placingTrap = false;
        EquippedWeaponID = weaponID;
        weaponIDText.text = "^ Weapon ID: " + EquippedWeaponID + " ^";

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

            _position = hit.point + Vector3.up * (trapBounds.y / 2);

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

                Debug.Log("Left: " + leftDist + " Right: " + rightDist);

            }
            else
            {
                
                _scale = new Vector3(1 * ScaleMaxLenght, 1, 1);

            }

        }
        else
        {

            _position = tempPos + Vector3.up * (trapBounds.y / 2);
            _scale = Vector3.one;

        }

        _rotation = transform.parent.rotation;

        return (_position, _scale, _rotation);

    }

    private (Vector3 position, Vector3 scale, Quaternion rotation) ScaleForward(Vector3 tempPos)
    {

        Vector3 _position, _scale;
        Quaternion _rotation;

        RaycastHit hit;
        if(Physics.Raycast(tempPos, Vector3.down, out hit, 1, mask, QueryTriggerInteraction.Ignore))
        {

            _position = hit.point + Vector3.up * (trapBounds.y / 2);

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

            _position = tempPos + Vector3.up * (trapBounds.y / 2);
            _scale = Vector3.one;

        }

        _rotation = transform.parent.rotation;

        return (_position, _scale, _rotation);

    }

    private (Vector3 position, Vector3 scale, Quaternion rotation) NoScaling(Vector3 tempPos)
    {

        Vector3 _position;
        Quaternion _rotation;

        RaycastHit hit;
        if(Physics.Raycast(tempPos, Vector3.down, out hit, 1, mask, QueryTriggerInteraction.Ignore))
        {

            _position = hit.point + Vector3.up * (trapBounds.y / 2);

        }
        else
        {

            _position = tempPos + Vector3.up * (trapBounds.y / 2);

        }

        //might wanna change this later, maybe?
        _rotation = transform.parent.rotation;

        return (_position, Vector3.zero, _rotation);

    }

    #endregion

    private void MeleeAttack()
    {



    }

    private void ThrowWeapon()
    {



    }

    private void RangedAttack()
    {



    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Mouse1) && EquippedWeaponID != 0)
        {

            if(!placingTrap)
            {

                placingTrap = true;
                ghostTrap = Instantiate(TrapPrefabs[EquippedWeaponID]).transform;
                trapBounds = ghostTrap.GetComponent<BoxCollider>().bounds.extents;
                boundsOffset = ghostTrap.GetComponent<BoxCollider>().bounds.center;

            }
            else
            {

                Transform temp = Instantiate(TrapPrefabs[EquippedWeaponID], placementInfo.Item1, placementInfo.Item3).transform;
                if(TrapScaleMode[EquippedWeaponID] != ScalingMode.None)
                    temp.localScale = placementInfo.Item2;
                Destroy(ghostTrap.gameObject);
                EquippedWeaponID = 0;
                weaponIDText.text = "^ Weapon ID: " + EquippedWeaponID + " ^";

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
        else if(EquippedWeaponID == 0)
        {

            placingTrap = false;

        }

    }
    
}