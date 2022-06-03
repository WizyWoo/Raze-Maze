using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;
using Photon.Pun;

public class PlayerColorPicker : MonoBehaviourPunCallbacks, IInteractable
{
    public int playerColor;
    public Renderer[] rends;
    public Material usedMat;

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
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material = usedMat;
        }
        
        this.enabled = false;
    }
}
