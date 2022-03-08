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

            ScriptToActivate = gameObject.GetComponent<WeaponController>();
            ScriptToActivate.FireWeapon(_firing);

        }

    }

    /*private void FixedUpdate()
    {

        if(!IsHeld)
        {

            if(ScriptToActivate != null)
            {

                ScriptToActivate.FireWeapon(false);

            }
            else
            {

                ScriptToActivate = gameObject.GetComponent<WeaponController>();
                ScriptToActivate.FireWeapon(false);

            }

        }

    }*/

}
