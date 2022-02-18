using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class DeathScript : MonoBehaviour
{
    private IEnumerator respawn;
    private int playerLives;

    private void OnTriggerEnter(Collider other)
    {
        respawn = FindObjectOfType<GameManager>().Respawn();
        playerLives = FindObjectOfType<PlayerController>().lives--;

        if (playerLives > 0)
            StartCoroutine(respawn);

        else
            FindObjectOfType<GameManager>().GameOver();
    }
}
