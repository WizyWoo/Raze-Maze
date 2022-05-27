using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyInstantiatorTest : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.InstantiateRoomObject("Key", transform.position, transform.rotation);
    }
}
