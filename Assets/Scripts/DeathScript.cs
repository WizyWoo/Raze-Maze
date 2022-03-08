using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class DeathScript : MonoBehaviour
{
    WeaponController wP;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<PlayerManager>().Damage(wP);
    }
}
