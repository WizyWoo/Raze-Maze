using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class BulletScript : MonoBehaviour
{
    private int playerLives;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<PlayerManager>().Damage();
        }
    }
}
