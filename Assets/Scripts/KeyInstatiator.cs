using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyInstatiator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.InstantiateRoomObject("Key", transform.position, transform.rotation);
    }
}
