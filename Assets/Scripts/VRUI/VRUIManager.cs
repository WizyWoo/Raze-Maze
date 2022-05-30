using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIManager : MonoBehaviour
{

    public float YOffset, ForwardOffset;
    public Transform MenuSpawnFrom;
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

            Vector3 _tempSpawnPos = MenuSpawnFrom.forward;
            _tempSpawnPos = new Vector3(_tempSpawnPos.x, 0, _tempSpawnPos.z).normalized * ForwardOffset;
            _tempSpawnPos = new Vector3(_tempSpawnPos.x, YOffset, _tempSpawnPos.z) + MenuSpawnFrom.position;
            transform.position = _tempSpawnPos;
            transform.rotation = new Quaternion(transform.rotation.x, MenuSpawnFrom.rotation.y, transform.rotation.z, MenuSpawnFrom.rotation.w);
            
        }

    }

}