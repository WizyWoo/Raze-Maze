using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSettings : MonoBehaviour , IInteractable
{

    public void Activate(Transform _player)
    {

        LocalGameController.main.RevertChanges();

    }

}
