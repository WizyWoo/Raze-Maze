using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HoldingAnchorThrowable : HoldingAnchor
{

    public float FollowSpeedMult = 50;
    public Collider MainCollider;

    private void Start()
    {

        rb = transform.root.GetComponent<Rigidbody>();
        MainCollider.isTrigger = false;

    }

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

    public override void Released()
    {
        
        IsHeld = false;
        Collider[] _cols = handTransform.root.GetComponentsInChildren<Collider>();
        handTransform = null;

        for(int i = 0; i < _cols.Length; i++)
        {

            for(int j = 0; j < Colliders.Length; j++)
            {

                Physics.IgnoreCollision(Colliders[j], _cols[i], false);

            }

        }

        rb.velocity = rb.velocity * 2;
        
    }

    private void Update()
    {

        if(IsHeld)
        {

            rb.velocity = (handTransform.position - transform.position) * FollowSpeedMult;
            transform.rotation = handTransform.rotation;

        }
        
    }

}
