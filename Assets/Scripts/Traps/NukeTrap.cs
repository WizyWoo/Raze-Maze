using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;
using Photon.Pun;

public class NukeTrap : TrapController
{

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip boomSound;
    [SerializeField]
    private GameObject explosionParticles;

    private void FixedUpdate()
    {
        
        if(TrapActived)
        {

            audioSource.PlayOneShot(boomSound);
            Instantiate(explosionParticles, transform.position, Quaternion.identity);

            GameManager.gameManager.player.GetComponent<PlayerManager>().Damage(null, Damage);

            Destroy(gameObject, boomSound.length);
            
            TrapActived = false;
            
        }

    }

}