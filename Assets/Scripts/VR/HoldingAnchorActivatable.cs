using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingAnchorActivatable : HoldingAnchor
{

    public WeaponController ScriptToActivate;

    public void ActivateInteractableOnObject(Transform _activatedBy, bool _firing)
    {

        if(ScriptToActivate != null)
        {

            ScriptToActivate.FireWeapon(_firing);

        }
        else
        {

            gameObject.GetComponent<WeaponController>().FireWeapon(_firing);

        }

    }

}
