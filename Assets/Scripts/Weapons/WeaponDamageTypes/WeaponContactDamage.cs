using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class WeaponContactDamage : WeaponController
{

    public bool ContinueslyDamage, CheckVelocity;
    [Tooltip("If velocity is higher than this, will damage player")]
    public float VelocityThreshold;
    private Rigidbody rb;

    private void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision _other)
    {

        if(!ContinueslyDamage)
        {

            if(_other.transform.root.TryGetComponent<IHit>(out IHit _tempPM) && (!CheckVelocity || rb.velocity.magnitude >= VelocityThreshold))
            {

                _tempPM.Damage(Damage);

            }

        }

    }

    private void OnCollisionStay(Collision _other)
    {

        if(ContinueslyDamage)
        {

            if(_other.transform.root.TryGetComponent<IHit>(out IHit _tempPM) && (!CheckVelocity || rb.velocity.magnitude >= VelocityThreshold))
            {

                _tempPM.Damage(Damage);

            }

        }

    }

}
