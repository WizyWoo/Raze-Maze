using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnchor : HoldingAnchorActivatable
{

    private bool pickedUp;

    public override HoldingAnchor Grabbed(Transform _grabbedBy)
    {
        
        if(!pickedUp)
        {

            transform.localScale = Vector3.one;
            transform.parent = null;

        }

        IsHeld = true;
        handTransform = _grabbedBy;

        if(rb)
            rb.isKinematic = true;

        return this;

    }

}
