using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MineTrap : ExplosiveTrapDamage
{

    private void OnTriggerEnter(Collider _other)
    {

        if(_other.transform.root.TryGetComponent<Rigidbody>(out Rigidbody _rb))
        {

            Explode();
            PhotonNetwork.Destroy(gameObject);

        }

    }

}
