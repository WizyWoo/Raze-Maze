using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Grenade : ExplosiveWeaponDamage
{

    public float FuseLenght;
    public GameObject ExplosionParticle, Pin, Body;
    private float fuseTime;

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

        if(Used && !ExplosiveUsed)
        {

            ExplosiveUsed = true;
            Explode();

            Destroy(Body);

            Invoke("DestroyMe", 10);

        }

    }

    public override void Explode()
    {

        PhotonNetwork.Instantiate(ExplosionParticle.name, transform.position, Quaternion.identity);
        
        base.Explode();

    }

    public void DestroyMe()
    {

        PhotonNetwork.Destroy(gameObject);

    }

    public override void Fire(bool _fire)
    {
        
        if(_fire)
        {

            fuseTime = FuseLenght;
            Destroy(Pin);

        }

    }

}
