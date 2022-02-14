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
            _itemController.PickedUpWeapon(ItemManager.main.RandomItemID());

            if(LockAfterUse)
            {

                locked = true;
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

            }
        
        }

    }

}
