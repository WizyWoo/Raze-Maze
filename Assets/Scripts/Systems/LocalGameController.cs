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
    public bool SnapTurning, MovementVignette;

    private void Awake()
    {

        if(LocalGameController.main != null)
            Destroy(gameObject);
        else
            main = this;

        DontDestroyOnLoad(this);

        RazeMazeSettings _settings = null;
        _settings = SaveAndLoad.LoadSettings(FileName);

        if(_settings == null)
        {

            SaveAndLoad.SaveSettings(FileName, RotationSpeed, false, false);

        }
        else
        {

            SnapTurning = _settings.SnapTurning;
            MovementVignette = _settings.MovementVignette;

        }

    }

    public bool WinCheck()
    {

        bool _temp = PlayerHasWon;

        PlayerHasWon = false;

        return _temp;

    }

    public void SaveCurrentSettings()
    {

        SaveAndLoad.SaveSettings(FileName, RotationSpeed, SnapTurning, MovementVignette);
        Debug.Log("Settings saved!");

    }

}
