using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class TrapTriggerDamage : TrapController
{

    public bool ContinueslyDamage;

    public override void ActivateTrap()
    {

        base.ActivateTrap();

        gameObject.GetComponent<Collider>().isTrigger = true;
        
    }

    private void FixedUpdate()
    {

        if(TrapActived)
            gameObject.GetComponent<Collider>().isTrigger = true;

    }
    
    private void OnTriggerEnter(Collider _other)
    {

        if(!ContinueslyDamage)
        {

            if(_other.transform.root.TryGetComponent<IHit>(out IHit _tempPM))
            {

                _tempPM.Damage(Damage);

            }

        }

    }

    private void OnTriggerStay(Collider _other)
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
