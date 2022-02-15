using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ContinuousGun : WeaponController, IPunObservable
{
    public ParticleSystem particleSystem;
    private bool isFiring  = false;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(isFiring);
        }
        else
        {
            // Network player, receive data
            this.isFiring = (bool)stream.ReceiveNext();
        }
    }

    void Update()
    {
        FireWeapon();

        if(isFiring == true)
            particleSystem.Play();
    }

    private void OnParticleCollision(GameObject other)
    {
        //int events = particleSystem.GetCollisionEvents(other, colEvents);

        //for (int i = 0; i < events; i++)
        //{

        //}
    }

    [PunRPC]
    public override void FireWeapon()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("shooooting");
                isFiring = true;
                //particleSystem.Play();
                //PhotonNetwork.Instantiate(particleSystem.name, transform.position, Quaternion.identity);
                //PhotonNetwork.Destroy(gameObject);
            }
            else
                isFiring = false;
        }

        //GameObject effectDefGo = PhotonNetwork.Instantiate(particleSystem.name, hit.point, Quaternion.LookRotation(hit.normal), 0);
        //effectDefGo.GetComponent<ParticleSystem>().Play();
    }
}
