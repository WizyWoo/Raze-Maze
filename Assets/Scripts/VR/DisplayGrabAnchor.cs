using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisplayGrabAnchor : HoldingAnchor
{

    public PlayerItemController ItemController;
    public bool IsWeaponDisplay;

    public override HoldingAnchor Grabbed(Transform _grabbedBy)
    {

        if(ItemController.EquippedWeaponID != 0)
        {

            if(IsWeaponDisplay)
            {

                ItemController.EquipWeapon(_grabbedBy);
                
                return null;

            }
            else
            {

                ItemController.StartPlacingTrap();

                return null;      

            }

        }
        else
            return null;

    }
    
}
