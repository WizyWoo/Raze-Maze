using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingAnchorActivatable : HoldingAnchor
{

    public WeaponController ScriptToActivate;

    public void ActivateInteractableOnObject(Transform _activatedBy, bool _active)
    {

        if(ScriptToActivate != null)
        {

            ScriptToActivate.FireWeapon(_active);

        }
        else
        {

            ScriptToActivate = gameObject.GetComponent<WeaponController>();
            ScriptToActivate.FireWeapon(_active);

        }

    }

    public override void Released()
    {

        base.Released();
        
        ScriptToActivate.FireWeapon(false);

    }

}
