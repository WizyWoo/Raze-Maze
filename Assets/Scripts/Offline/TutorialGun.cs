using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGun : MonoBehaviour , IActivatable
{

    public Transform RaycastPoint;
    public float Damage;
    private bool firing;
    private LayerMask weaponMask;

    private void Start()
    {

        weaponMask = ~(1 << LayerMask.NameToLayer("Interactables"));

    }

    public void Activate(bool _OnOff)
    {

        firing = _OnOff;

    }

    private void Update()
    {

        if(firing)
        {

            if(Physics.Raycast(RaycastPoint.position, RaycastPoint.forward, out RaycastHit _hit, Mathf.Infinity, weaponMask, QueryTriggerInteraction.Collide))
            {

                if(_hit.transform.gameObject.layer == LayerMask.NameToLayer("TakesDamage"))
                {

                    _hit.transform.GetComponent<IHit>().Damage(Damage);

                }

            }

        }

    }

}
