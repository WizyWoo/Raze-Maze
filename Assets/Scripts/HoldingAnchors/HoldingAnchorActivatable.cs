using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingAnchorActivatable : HoldingAnchor
{

    public IActivatable ScriptToActivate;

    public void ActivateInteractableOnObject(Transform _activatedBy, bool _active)
    {

        if(ScriptToActivate != null)
        {

            ScriptToActivate.Activate(_active);

        }
        else
        {

            ScriptToActivate = gameObject.GetComponent<IActivatable>();
            ScriptToActivate.Activate(_active);

        }

    }

    public override void Released()
    {

        base.Released();
        
        ScriptToActivate.Activate(false);

    }

}
