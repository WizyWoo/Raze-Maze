using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{

    public GameObject[] WeaponPrefabs;
    public bool[] TrapRescaledOnUse;
    public float TrapPlaceDistance, PlacementCheckRange;
    public int EquippedWeaponID {get; private set;}
    [SerializeField]
    private GameObject droppedWeaponPrefab;
    [SerializeField]
    private Text weaponIDText;
    private bool placingTrap, validLocation;
    private Vector3 placingTrapPos, placeOffset;
    private Transform ghostTrap;
    private Vector3 ghostBounds;
    private LayerMask mask;

    private void Start()
    {

        mask = LayerMask.NameToLayer("Player") + LayerMask.NameToLayer("Traps");

    }

    public void PickedUpWeapon(int weaponID)
    {

        if(EquippedWeaponID != 0)
            Instantiate(droppedWeaponPrefab, transform.position, Quaternion.identity).GetComponent<WeaponPickup>().WeaponID = EquippedWeaponID;
        
        EquippedWeaponID = weaponID;

    }

    private void ScaleTrapToRoom(Vector3 tempPos)
    {

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

            }
            else
                ghostTrap.localScale = Vector3.one;

        }

        ghostTrap.position = tempPos;
        placingTrapPos = tempPos;

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Mouse1) && EquippedWeaponID != 0)
        {

            if(!placingTrap)
            {

                placingTrap = true;
                GameObject temp = Instantiate(WeaponPrefabs[EquippedWeaponID], transform.position, Quaternion.identity);
                ghostBounds = temp.GetComponent<BoxCollider>().bounds.extents;
                ghostTrap = temp.transform;

            }
            else
            {

                placingTrap = false;
                Destroy(ghostTrap.gameObject);
                ghostTrap = null;
                Instantiate(WeaponPrefabs[EquippedWeaponID], placingTrapPos + placeOffset, Quaternion.identity);
                EquippedWeaponID = 0;

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