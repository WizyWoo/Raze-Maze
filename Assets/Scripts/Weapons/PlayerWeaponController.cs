using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
    //TODO
    /*
    - Sepperate functions for the different scaling
    - Make forward scaling
    - Have a "Weapon editor" where you can change settings for individual weapons
    */

    public enum ScalingMode
    {

        Normal,
        Forward,
        Sideways

    }

    public GameObject[] WeaponPrefabs;
    public bool[] TrapRescaledOnUse;
    public float TrapPlaceDistance, PlacementCheckRange;
    public int EquippedWeaponID {get; private set;}
    [SerializeField]
    private GameObject droppedWeaponPrefab;
    [SerializeField]
    private Text weaponIDText;
    private bool placingTrap, validLocation;
    [SerializeField]
    private Vector3 placingTrapPos, placeOffset, placingScale;
    private Quaternion placingRotation;
    private Transform ghostTrap;
    private Vector3 ghostBounds;
    private LayerMask mask;

    private void Start()
    {

        mask = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Traps")));
        placingScale = Vector3.one;

    }

    public void PickedUpWeapon(int weaponID)
    {

        if(EquippedWeaponID != 0)
            Instantiate(droppedWeaponPrefab, transform.position, Quaternion.identity).GetComponent<WeaponPickup>().WeaponID = EquippedWeaponID;
        
        placingTrap = false;
        EquippedWeaponID = weaponID;

    }

    private void ScaleTrapToRoom(Vector3 tempPos)
    {

        //might need changing when moving to vr!
        if(transform.parent)
        {

            ghostTrap.rotation = transform.parent.rotation;
            placingRotation = transform.parent.rotation;

        }
        else
        {

            ghostTrap.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
            placingRotation = Quaternion.Euler(0, transform.rotation.y, 0);

        }

        RaycastHit hit;
        if(Physics.Raycast((tempPos + (Vector3.up * 2)), Vector3.down, out hit, PlacementCheckRange, mask, QueryTriggerInteraction.Ignore))
        {

            tempPos = new Vector3(tempPos.x, hit.point.y + ghostBounds.y, tempPos.z);

        }

        if(TrapRescaledOnUse[EquippedWeaponID])
        {

            float rightDist = 0, leftDist = 0;

            if(Physics.Raycast(tempPos, ghostTrap.right, out hit, PlacementCheckRange, mask, QueryTriggerInteraction.Ignore))
            {

                rightDist = Vector3.Distance(tempPos, hit.point);

            }
            if(Physics.Raycast(tempPos, ghostTrap.right * -1, out hit, PlacementCheckRange, mask, QueryTriggerInteraction.Ignore))
            {

                leftDist = Vector3.Distance(tempPos, hit.point);

            }

            if(rightDist != 0 && leftDist != 0)
            {

                float scale;

                if(rightDist > leftDist)
                    scale = leftDist * 2;
                else
                    scale = rightDist * 2;

                ghostTrap.localScale = new Vector3(scale, 1, 1);
                placingScale = new Vector3(scale, 1, 1);

            }
            else
            {

                ghostTrap.localScale = Vector3.one;
                placingScale = Vector3.one;

            }

        }
        else
        {

            ghostTrap.localScale = Vector3.one;
            placingScale = Vector3.one;

        }

        ghostTrap.position = tempPos - placeOffset;
        placingTrapPos = tempPos;

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Mouse1) && EquippedWeaponID != 0)
        {

            if(!placingTrap)
            {

                placingTrap = true;
                ghostTrap = Instantiate(WeaponPrefabs[EquippedWeaponID], transform.position, Quaternion.identity).transform;
                ghostBounds = ghostTrap.GetComponent<BoxCollider>().bounds.extents;
                placeOffset = ghostTrap.GetComponent<BoxCollider>().center;

            }
            else
            {

                placingTrap = false;
                GameObject temp = Instantiate(WeaponPrefabs[EquippedWeaponID], placingTrapPos - placeOffset, placingRotation);
                temp.transform.localScale = placingScale;
                EquippedWeaponID = 0;
                Destroy(ghostTrap.gameObject);
                ghostTrap = null;

            }

        }

        weaponIDText.text = "^ " + EquippedWeaponID + " ^";

    }

    private void FixedUpdate()
    {

        if(placingTrap && EquippedWeaponID != 0)
        {

            Vector3 tempV3;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, TrapPlaceDistance, mask, QueryTriggerInteraction.Ignore))
            {

                tempV3 = hit.point;

            }
            else
            {

                tempV3 = transform.position + (transform.forward * TrapPlaceDistance);

            }

            ScaleTrapToRoom(tempV3);

        }
        else if(EquippedWeaponID == 0)
        {

            placingTrap = false;

        }

    }
    
}