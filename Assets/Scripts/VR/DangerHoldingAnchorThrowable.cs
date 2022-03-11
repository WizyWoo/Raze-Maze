using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerHoldingAnchorThrowable : HoldingAnchor
{

    public bool ActivateWhenThrown;
    public float FollowSpeedMult = 50;
    public WeaponController ScriptToActivate;
    public Collider MainCollider;

    private void Start()
    {

        rb = transform.root.GetComponent<Rigidbody>();
        MainCollider.isTrigger = false;

    }

    public override HoldingAnchor Grabbed(Transform _grabbedBy)
    {

        IsHeld = true;
        handTransform = _grabbedBy;
        MainCollider.isTrigger = true;

        return this;

    }

    public override void Released()
    {
        
        IsHeld = false;
        handTransform = null;
        MainCollider.isTrigger = true;

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
