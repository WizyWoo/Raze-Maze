using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : MonoBehaviour , IInteractable
{

    public ItemManager.Theme PickupTheme;
    public bool LockAfterUse;
    private bool locked;

    public void Activate(Transform _player)
    {
        
        if(!locked)
        {
            
            ItemManager.main.GiveRandomFilteredID(PickupTheme, _player.root.GetComponentInChildren<PlayerItemController>());

            if(LockAfterUse)
            {

                locked = true;
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

            }
        
        }

    }

}
