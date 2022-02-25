using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                HoldingAnchor _tempAnchor = ItemController.EquipWeapon(_grabbedBy);
                
                return _tempAnchor;

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
