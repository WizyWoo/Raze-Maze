using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grenade : WeaponController
{

    public float FuseLenght;
    public GameObject ExplosionParticle;
    private float fuseTime;

    public override void Thrown()
    {

        fuseTime = FuseLenght;
        gameObject.GetComponent<Collider>().isTrigger = false;

    }

    private void Update()
    {

        if(fuseTime > 0)
        {

            fuseTime -= Time.deltaTime;
            if(fuseTime <= 0)
            {

                Explode();

            }

        }

    }

    private void Explode()
    {

        PhotonNetwork.Instantiate(ExplosionParticle.name, transform.position, Quaternion.identity);
        //player damage logic here later
        PhotonNetwork.Destroy(gameObject);

    }

}
