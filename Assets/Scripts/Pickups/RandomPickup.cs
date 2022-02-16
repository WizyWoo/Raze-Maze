using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : MonoBehaviour , IInteractable
{

    public bool LockAfterUse;
    private bool locked;

    public void Activate(Transform _player)
    {
        
        if(!locked)
        {
            
            ItemManager.main.GivePlayerRandomWeapon(_player.GetComponent<PlayerItemController>());

            if(LockAfterUse)
            {

                locked = true;
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

            }
        
        }

    }

}
