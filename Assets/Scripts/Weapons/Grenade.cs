using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : WeaponController
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

                Explode();

            }

        }

    }

    private void OnTriggerExit()
    {

        gameObject.GetComponent<Collider>().isTrigger = false;

    }

    private void Explode()
    {

        Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
        //player damage logic here later
        Destroy(gameObject);

    }

}
