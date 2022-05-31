using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;
using Photon.Pun;

public class NukeTrap : TrapController
{

    [SerializeField]
    private GameObject explosionParticles;
    private bool used;

    private void FixedUpdate()
    {

        if(TrapActived && !used)
            NukeTriggered();

    }

    private void NukeTriggered()
    {

        used = true;
        //audioSource.PlayOneShot(boomSound);
        Instantiate(explosionParticles, transform.position, Quaternion.identity);

        GameManager.gameManager.player.GetComponent<IHit>().Damage(Damage);

        Destroy(gameObject, 10);
        PlaySound(1);

    }

}
