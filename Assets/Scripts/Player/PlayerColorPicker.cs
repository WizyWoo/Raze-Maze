using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;
using Photon.Pun;

public class PlayerColorPicker : MonoBehaviourPunCallbacks, IInteractable
{
    public int playerColor;

    public void Activate(Transform player)
    {
        if(player.root.GetComponent<PlayerManager>().colorId == 0)
        {
            player.root.GetComponent<PlayerManager>().ChangeColorId(playerColor);
            photonView.RPC("ColorChecker", RpcTarget.All);
        }
    }

    [PunRPC]
    public void ColorChecker()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.grey;
        this.enabled = false;
    }
}
