using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class BulletScript : MonoBehaviour
{
    private int playerLives;
    public WeaponController wP;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<IHit>().Damage(wP.Damage);
        }
    }
}
