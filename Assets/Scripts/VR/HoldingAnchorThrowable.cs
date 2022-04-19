using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingAnchorThrowable : HoldingAnchor
{

    public bool ActivateWhenThrown;
    public float FollowSpeedMult = 50;
    public WeaponController ScriptToActivate;
    public Collider MainCollider;
    private Transform grabbedBy;

    private void Start()
    {

        rb = transform.root.GetComponent<Rigidbody>();
        MainCollider.isTrigger = false;

    }

    public override HoldingAnchor Grabbed(Transform _grabbedBy)
    {

        IsHeld = true;
        handTransform = _grabbedBy;

        Collider[] _cols = _grabbedBy.root.GetComponentsInChildren<Collider>();

        for(int i = 0; i < _cols.Length; i++)
        {

            Physics.IgnoreCollision(MainCollider, _cols[i], true);

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

            Physics.IgnoreCollision(MainCollider, _cols[i], false);

        }

        if(ActivateWhenThrown)
        {

            ScriptToActivate.Thrown();

        }
        
    }

    private void Update()
    {

        if(IsHeld)
        {

            rb.velocity = (handTransform.position - transform.position) * FollowSpeedMult;

        }
        
    }

}
