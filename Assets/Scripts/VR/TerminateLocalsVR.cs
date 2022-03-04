using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor.XR.LegacyInputHelpers;

public class TerminateLocalsVR : MonoBehaviourPunCallbacks
{

    public GameObject[] DestroyThis;

    private void Awake()
    {

        if(!photonView.IsMine)
        {

            foreach (GameObject go in DestroyThis)
            {

                Destroy(go);
                
            }

            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<PlayerItemController>());
            Destroy(gameObject.GetComponent<VrPlayerController>());
            Destroy(gameObject.GetComponent<CameraOffset>());
            Destroy(gameObject.GetComponentInChildren<PlayerInteraction>());
            
            Destroy(gameObject.GetComponentInChildren<Camera>());
            
        }

        Destroy(this);

    }

}
