using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectOffline : MonoBehaviour , IHit
{

    public float ObjectHealth;

    public void Damage(float _damageAmount)
    {

        ObjectHealth -= _damageAmount;

        if(ObjectHealth <= 0)
        {

            Destroy(gameObject);

        }

    }

}
