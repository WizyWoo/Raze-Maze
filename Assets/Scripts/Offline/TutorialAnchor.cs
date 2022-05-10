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

            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            transform.parent = null;

        }

        IsHeld = true;
        handTransform = _grabbedBy;

        if(rb)
            rb.isKinematic = true;

        return this;

    }

}
