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

            this.gameObject.tag = "NotMe";
            foreach (GameObject go in TerminateThis)
            {

                Destroy(go);
                
            }

            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<PlayerController>());
            Destroy(gameObject.GetComponent<PlayerPositionLog>());

        }

        Destroy(this);

    }

}
