using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyHoldingAnchor : HoldingAnchor
{

    public override HoldingAnchor Grabbed(Transform _grabbedBy)
    {

        PView.TransferOwnership(_grabbedBy.transform.root.GetComponent<PhotonView>().ViewID);

        IsHeld = true;
        handTransform = _grabbedBy;

        if(rb)
            rb.isKinematic = true;

        return this;
        
    }

}
