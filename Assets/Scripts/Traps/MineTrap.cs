using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MineTrap : ExplosiveTrapDamage
{

    public ParticleSystem ParticleSys;

    private void OnTriggerEnter(Collider _other)
    {

        if(_other.transform.root.TryGetComponent<Rigidbody>(out Rigidbody _rb) && !Used)
        {

            Explode();

            ParticleSys.Play();
            
            Used = true;
            Invoke("DestroyMe", 10);

        }

    }

    private void DestroyMe()
    {

        PhotonNetwork.Destroy(gameObject);

    }

}
