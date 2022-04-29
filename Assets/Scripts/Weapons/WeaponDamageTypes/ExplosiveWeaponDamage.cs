using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class ExplosiveWeaponDamage : WeaponController
{

    [Tooltip("If you want the player to die instantly, set damage => than radius")]
    public float ExplosionRadius;

    public virtual void Explode()
    {

        Collider[] _colHits;

        _colHits = Physics.OverlapSphere(transform.position, ExplosionRadius, 1 << LayerMask.NameToLayer("Player"), QueryTriggerInteraction.Collide);

        foreach(Collider _col in _colHits)
        {

            Debug.Log(_col.transform.name + " was inside the radius");

            if(Physics.Linecast(transform.position, _col.transform.position, out RaycastHit _hit, WeaponMask, QueryTriggerInteraction.Collide))
            {

                Debug.Log(_hit.transform.name);

                if(_hit.transform.root.TryGetComponent<IHit>(out IHit _tempHit))
                {

                    _tempHit.Damage(Damage / Vector3.Distance(_hit.transform.position, transform.position));

                }

            }

        }

    }

}
