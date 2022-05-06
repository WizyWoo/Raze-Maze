using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ContinuousGun : WeaponController, IPunObservable
{
    public ParticleSystem particleSystemu;
    private bool isFiring  = false;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    void Update()
    {

        if(Firing == true)
            particleSystemu.Play();
    }

    private void OnParticleCollision(GameObject other)
    {
        //int events = particleSystem.GetCollisionEvents(other, colEvents);

        //for (int i = 0; i < events; i++)
        //{

        //}
    }

    //[PunRPC]
    /*public override void FireWeapon(bool _firing)
    {
        if (photonView.IsMine)
        {
            
            Firing = _firing;

        }

        //GameObject effectDefGo = PhotonNetwork.Instantiate(particleSystem.name, hit.point, Quaternion.LookRotation(hit.normal), 0);
        //effectDefGo.GetComponent<ParticleSystem>().Play();
    }*/
}
