using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIManager : MonoBehaviour
{

    public Transform MenuSpawnPoint;
    public GameObject VRMenu;
    public VrPlayerController MovementController;
    private bool menuOpen;

    public void ToggleUIMode()
    {

        menuOpen = !menuOpen;

        VRMenu.SetActive(menuOpen);
        MovementController.enabled = !menuOpen;

        if(menuOpen)
        {

            transform.position = MenuSpawnPoint.position;
            
        }

    }

}
