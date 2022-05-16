using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameController : MonoBehaviour
{

    public static LocalGameController main { get; private set; }
    public bool PlayerHasWon;
    public string FileName;

    //Settings
    public float RotationSpeed;
    public int DegreesPerRotate;
    public bool SnapTurning, MovementVignette;
    public VrPlayerController PlayerController;

    private void Awake()
    {

        if(LocalGameController.main != null)
            Destroy(gameObject);
        else
            main = this;

        DontDestroyOnLoad(this);

    }

    public void LoadSettings()
    {

        RazeMazeSettings _settings = null;
        _settings = SaveAndLoad.LoadSettings(FileName);

        if(_settings == null)
        {

            SaveAndLoad.SaveSettings(FileName);
            _settings = SaveAndLoad.LoadSettings(FileName);

        }

        RotationSpeed = _settings.RotationSpeed;
        DegreesPerRotate = _settings.DegreesPerRotate;
        SnapTurning = _settings.SnapTurning;
        MovementVignette = _settings.MovementVignette;

        if(PlayerController)
            PlayerController.UpdatePlayerController();

    }

    public bool WinCheck()
    {

        bool _temp = PlayerHasWon;

        PlayerHasWon = false;

        return _temp;

    }

    public void SaveCurrentSettings()
    {

        SaveAndLoad.SaveSettings(FileName, RotationSpeed, DegreesPerRotate, SnapTurning, MovementVignette);
        Debug.Log("Settings saved!");

    }

}
