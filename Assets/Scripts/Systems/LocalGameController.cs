using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameController : MonoBehaviour
{

    public static LocalGameController main { get; private set; }
    public bool PlayerHasWon;
    public SaveAndLoad SavingAndLoading;

    //Settings
    public bool SnapTurning, MovementVignette;

    private void Awake()
    {

        if(LocalGameController.main != null)
            Destroy(gameObject);
        else
            main = this;

        DontDestroyOnLoad(this);

        RazeMazeSettings _settings = SavingAndLoading.LoadSettings();

        if(_settings == null)
        {

            SavingAndLoading.SaveSettings(false, false);

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

        SavingAndLoading.SaveSettings(SnapTurning, MovementVignette);
        Debug.Log("Settings saved!");

    }

}
