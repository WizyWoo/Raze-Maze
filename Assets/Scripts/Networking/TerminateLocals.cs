using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TerminateLocals : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject[] TerminateThis;

    private void Awake()
    {

        if(!photonView.IsMine)
        {

            foreach (GameObject go in TerminateThis)
            {

                Destroy(go);
                
            }

        }

    }

}
