using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : MonoBehaviour , IInteractable
{

    public ItemManager.Theme PickupTheme;
    public bool LockAfterUse;
    public Material UsedMat;
    private bool locked;

    //Using weapon displays
    /*private void OnTriggerEnter(Collider col)
    {

        if(col.transform.root.tag == "Player")
            if(col.transform.root.GetComponentInChildren<PlayerItemController>().EquippedWeaponID == 0 && col.transform.root.GetComponentInChildren<PlayerItemController>().CurrentItemID == 0)
                Activate(col.transform.root);

    }*/

    public void Activate(Transform _player)
    {

        PlayerInteraction.Hand _handLR = _player.GetComponent<PlayerInteraction>().HandLR;
        
        if(!locked)
        {

            ItemManager.main.GiveRandomFilteredID(PickupTheme, _player.root.GetComponentInChildren<PlayerItemController>());

            //New stuff
            if(_handLR == PlayerInteraction.Hand.Left)
            {

                Debug.Log("1");

                _player.root.GetComponent<PlayerItemController>().StartPlacingTrap();

            }
            else
            {

                HoldingAnchor _tempAnchor = _player.root.GetComponent<PlayerItemController>().EquipWeapon(_player);
                _tempAnchor.Released();

            }
            //End of new stuff

            if(LockAfterUse)
            {

                locked = true;
                gameObject.GetComponent<MeshRenderer>().material = UsedMat;

            }
        
        }

    }

}