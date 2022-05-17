using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings : MonoBehaviour , IInteractable
{

    public void Activate(Transform _player)
    {

        LocalGameController.main.SaveCurrentSettings();

    }

}
