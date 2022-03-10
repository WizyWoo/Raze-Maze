using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class ExplosiveTrapDamage : TrapController
{

    public float ExplosionRadius;

    public virtual void Explode()
    {

        Collider[] _colHits;

        _colHits = Physics.OverlapSphere(transform.position, ExplosionRadius, LayerMask.NameToLayer("Player"), QueryTriggerInteraction.Ignore);

        foreach(Collider _col in _colHits)
        {

            RaycastHit _hit;
            Physics.Linecast(transform.position, _col.transform.position, out _hit, LayerMask.NameToLayer("Interactables"), QueryTriggerInteraction.Ignore);

            if(_hit.transform.root.TryGetComponent<PlayerManager>(out PlayerManager _tempPlayerManager))
            {

                _tempPlayerManager.Damage(Damage);

            }
            else
            {

                Debug.Log(_hit.transform.name);

            }

        }

    }

}