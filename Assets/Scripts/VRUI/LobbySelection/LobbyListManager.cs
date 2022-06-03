using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbyListManager : MonoBehaviourPunCallbacks
{

    public int Rooms;

    private void Start()
    {

        Rooms = PhotonNetwork.CountOfRooms;

    }

}
