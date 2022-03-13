using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class TrapContactDamage : TrapController
{

    public bool ContinueslyDamage;

    public override void ActivateTrap()
    {

        base.ActivateTrap();

        gameObject.GetComponent<Collider>().isTrigger = false;
        
    }

    private void FixedUpdate()
    {

        if(TrapActived)
            gameObject.GetComponent<Collider>().isTrigger = false;

    }
    
    private void OnCollisionEnter(Collision _other)
    {

        if(!ContinueslyDamage)
        {

            if(_other.transform.root.TryGetComponent<PlayerManager>(out PlayerManager _tempPM))
            {

                _tempPM.Damage(Damage);

            }

        }

    }

    private void OnCollisionStay(Collision _other)
    {

        if(ContinueslyDamage)
        {

            if(_other.transform.root.TryGetComponent<PlayerManager>(out PlayerManager _tempPM))
            {

                _tempPM.Damage(Damage);

            }

        }

    }

}
