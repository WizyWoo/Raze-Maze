using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour , IInteractable
{

    public GameObject CheckMark;
    private bool settingOnOff;

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

        settingOnOff = !settingOnOff;
        CheckMark.SetActive(settingOnOff);

    }

    private void Start()
    {

        switch (SettingToChange)
        {
            
            case SettingValue.SnapTurning:
            settingOnOff = LocalGameController.main.SettingsData.SnapTurning;
            break;

            case SettingValue.MovementVignette:
            settingOnOff = LocalGameController.main.SettingsData.MovementVignette;
            break;

        }

        CheckMark.SetActive(settingOnOff);

    }

}
