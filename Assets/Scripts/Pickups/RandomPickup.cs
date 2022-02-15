using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : MonoBehaviour , IInteractable
{

    public bool LockAfterUse;
    private bool locked;

    public void Activate(Transform player)
    {
        
        if(!locked)
        {
           
            PlayerWeaponController _itemController = player.GetComponent<PlayerWeaponController>();
            Random.InitState(System.DateTime.UtcNow.Second);
            _itemController.PickedUpWeapon(Random.Range(1, _itemController.TrapPrefabs.Length));

            //_itemController.PickedUpWeapon(ItemManager.main.RandomItemID());

            if(LockAfterUse)
            {

                locked = true;
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

            }
        
        }

    }

}
