using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyHoldingAnchor : HoldingAnchorThrowable
{
    
    public PhotonView PView;

    public override HoldingAnchor Grabbed(Transform _grabbedBy)
    {

        PView.TransferOwnership(_grabbedBy.transform.root.GetComponent<PhotonView>().ViewID);

        IsHeld = true;
        handTransform = _grabbedBy;

        Collider[] _cols = handTransform.root.GetComponentsInChildren<Collider>();

        for(int i = 0; i < _cols.Length; i++)
        {

            for(int j = 0; j < Colliders.Length; j++)
            {

                Physics.IgnoreCollision(Colliders[j], _cols[i], true);

            }

        }

        return this;
        
    }

}
