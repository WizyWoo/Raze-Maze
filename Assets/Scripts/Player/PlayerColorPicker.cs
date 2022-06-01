using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class PlayerColorPicker : MonoBehaviour, IInteractable
{
    public int playerColor;

    public void Activate(Transform player)
    {
        player.root.GetComponent<PlayerManager>().ChangeColorId(playerColor);
    }
}
