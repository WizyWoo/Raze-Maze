using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour , IInteractable
{

    public bool CurrentValue;

    public enum SettingValue
    {

        SnapTurning,
        MovementVignette

    }

    public SettingValue SettingToChange;

    public void Activate(Transform _player)
    {

        switch (SettingToChange)
        {
            
            case SettingValue.SnapTurning:
            LocalGameController.main.SnapTurning();
            break;

            case SettingValue.MovementVignette:
            LocalGameController.main.MovementVignette();
            break;

        }

    }

}
