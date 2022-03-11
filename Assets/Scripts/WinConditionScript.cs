using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class WinConditionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.gameManager.WinLevel();
    }
}
