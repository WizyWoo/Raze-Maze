using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour , IHit
{

    private IHit hitParent;

    private void Start()
    {

        hitParent = transform.root.GetComponent<IHit>();

    }

    public void Damage(float _damageAmount)
    {

        hitParent.Damage(_damageAmount);

    }

}
