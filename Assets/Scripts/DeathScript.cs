using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class DeathScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<PlayerManager>().Damage();
    }
}
