using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ContinuousGun : WeaponController
{
    public ParticleSystem particleSystem;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    void Update()
    {
        FireWeapon();
    }

    private void OnParticleCollision(GameObject other)
    {
        //int events = particleSystem.GetCollisionEvents(other, colEvents);

        //for (int i = 0; i < events; i++)
        //{

        //}
    }

    public override void FireWeapon()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("shooooting");
            particleSystem.Play();
            //PhotonNetwork.Instantiate(particleSystem.name, transform.position, Quaternion.identity);
            //PhotonNetwork.Destroy(gameObject);
        }
    }
}
