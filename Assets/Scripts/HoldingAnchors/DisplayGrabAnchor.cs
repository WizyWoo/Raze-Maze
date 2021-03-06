using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGrabAnchor : HoldingAnchor
{

    public PlayerItemController ItemController;
    public bool IsWeaponDisplay;
    public GameObject ItemDisplay;

    public void UpdateDisplayModel(GameObject _displayPrefab)
    {

        Destroy(ItemDisplay);
        ItemDisplay = Instantiate(_displayPrefab, transform);

        if(ItemDisplay.TryGetComponent<Rigidbody>(out Rigidbody _rb))
        {

            Destroy(_rb);

        }

        if(ItemDisplay.TryGetComponent<Collider>(out Collider _col))
        {

            Collider[] _tempColliders = ItemDisplay.GetComponentsInChildren<Collider>();

            for(int i = 0; i < _tempColliders.Length; i++)
            {

                Destroy(_tempColliders[i]);

            }

        }

    }

    public override HoldingAnchor Grabbed(Transform _grabbedBy)
    {

        if(ItemController.CurrentItemID != 0)
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

    public override void Released(){}
    
}
