using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTrap : MonoBehaviour , IHit
{

    public float TrapHealth;

    public void Damage(float _amount)
    {

        TrapHealth -= _amount;

        if(TrapHealth <= 0)
        {

            Destroy(gameObject);

        }

    }

}
