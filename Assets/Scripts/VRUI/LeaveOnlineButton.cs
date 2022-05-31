using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class LeaveOnlineButton : MonoBehaviour , IInteractable
{

    public void Activate(Transform _player)
    {

        LocalGameController.main.SaveCurrentSettings();
        GameManager.gameManager.LeaveRoom();

    }

}
