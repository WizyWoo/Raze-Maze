using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grenade : ExplosiveWeaponDamage
{

    public float FuseLenght;
    public GameObject ExplosionParticle;
    private float fuseTime;

    public override void Thrown()
    {

        fuseTime = FuseLenght;

    }

    private void Update()
    {

        if(fuseTime > 0)
        {

            fuseTime -= Time.deltaTime;
            if(fuseTime <= 0)
            {

                Used = true;

            }

        }

        if(Used)
        {

            //Explode();

        }

    }

    public override void Explode()
    {

        PhotonNetwork.Instantiate(ExplosionParticle.name, transform.position, Quaternion.identity);
        
        base.Explode();

        Destroy(gameObject);

    }

}
