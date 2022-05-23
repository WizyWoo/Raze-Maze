using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameController : MonoBehaviour
{

    public static LocalGameController main { get; private set; }
    public bool PlayerHasWon;
    public string FileName;

    //Settings
    public RazeMazeSettings SettingsData;
    public VrPlayerController PlayerController;

    private void Awake()
    {

        if(LocalGameController.main != null)
            Destroy(gameObject);
        else
            main = this;

        DontDestroyOnLoad(this);

    }

    public RazeMazeSettings LoadSettings()
    {

        SettingsData = SaveAndLoad.LoadSettings(FileName);

        if(SettingsData == null)
        {

            SaveAndLoad.SaveSettings(FileName, null);
            SettingsData = SaveAndLoad.LoadSettings(FileName);

        }

        return SettingsData;

    }

    public void SaveCurrentSettings()
    {

        SaveAndLoad.SaveSettings(FileName, SettingsData);
        PlayerController.Vals = SettingsData;

    }

    public void RevertChanges()
    {

        SettingsData = SaveAndLoad.LoadSettings(FileName);
        PlayerController.Vals = SettingsData;

    }

    public bool WinCheck()
    {

        bool _temp = PlayerHasWon;

        PlayerHasWon = false;

        return _temp;

    }

    public void RotationSpeed(float _speed) => SettingsData.RotationSpeed = _speed;
    public void DegreesPerRotate(int _degrees) => SettingsData.DegreesPerRotate = _degrees;
    public void SnapTurning(bool _onOff) => SettingsData.SnapTurning = _onOff;
    public void MovementVignette(bool _onOff) => SettingsData.MovementVignette = _onOff;

}
