using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{

    public GameObject[] WeaponPrefabs;
    public bool[] TrapRescaledOnUse;
    public float TrapPlaceDistance, PlacementCheckRange;
    public int EquippedWeaponID {get; private set;}
    [SerializeField]
    private GameObject droppedWeaponPrefab;
    private bool placingTrap, validLocation;
    private Vector3 placingTrapPos, placeOffset;
    private Transform ghostTrap;
    private Vector3 ghostBounds;
    private LayerMask mask;

    private void Start()
    {

        

    }

    public void PickedUpWeapon(int weaponID)
    {

        Instantiate(droppedWeaponPrefab, transform.position, Quaternion.identity).GetComponent<WeaponPickup>().WeaponID = EquippedWeaponID;
        EquippedWeaponID = weaponID;

    }

    private void ScaleTrapToRoom()
    {

        RaycastHit hit;

        if(Physics.Raycast(placingTrapPos, Vector3.down, out hit, PlacementCheckRange, mask, QueryTriggerInteraction.Ignore))
        {

            placingTrapPos = new Vector3(placingTrapPos.x, hit.point.y, placingTrapPos.z);

        }

        ghostTrap.position = placingTrapPos;

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

            }
            else
            {

                placingTrap = false;
                Destroy(ghostTrap);
                ghostTrap = null;
                Instantiate(WeaponPrefabs[EquippedWeaponID], placingTrapPos + placeOffset, Quaternion.identity);
                EquippedWeaponID = 0;

            }

        }

    }

    private void FixedUpdate()
    {

        if(placingTrap && EquippedWeaponID != 0)
        {

            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.forward, out hit, TrapPlaceDistance, mask, QueryTriggerInteraction.Ignore))
            {

                placingTrapPos = hit.point;

            }
            else
            {

                placingTrapPos = transform.position + (transform.forward * TrapPlaceDistance);

            }

            ScaleTrapToRoom();

        }
        else if(EquippedWeaponID == 0)
        {

            placingTrap = false;

        }

    }
    
}