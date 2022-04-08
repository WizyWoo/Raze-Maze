using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : MonoBehaviour , IInteractable
{

    public ItemManager.Theme PickupTheme;
    public bool LockAfterUse;
    private bool locked;

    private void OnTriggerEnter(Collider col)
    {

        if(col.transform.root.tag == "Player")
            if(col.transform.root.GetComponentInChildren<PlayerItemController>().EquippedWeaponID == 0 && col.transform.root.GetComponentInChildren<PlayerItemController>().CurrentItemID == 0)
                Activate(col.transform.root);

    }

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
