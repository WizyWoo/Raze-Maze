using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingAnchorActivatable : HoldingAnchor
{

    public IInteractable ScriptToActivate;

    public void ActivateInteractableOnObject(Transform _activatedBy)
    {

        if(ScriptToActivate != null)
        {

            ScriptToActivate.Activate(_activatedBy);

        }
        else
        {

            gameObject.GetComponent<IInteractable>().Activate(_activatedBy);

        }

    }

}
