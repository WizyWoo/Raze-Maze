using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveAndLoad : MonoBehaviour
{

    ///<Summary>
    /// Tries to find a RazeMazeSettings file by the specified FileName (_path) in the Appdata folder (not sure what it's called on mac but works there too :3)
    ///</Summary>
    public static RazeMazeSettings LoadSettings(string _path)
    {

        string _loadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _path);

        if(File.Exists(_loadPath))
        {

            BinaryFormatter _formatter = new BinaryFormatter();
            FileStream _stream = new FileStream(_loadPath, FileMode.Open, FileAccess.Read);

            RazeMazeSettings _razeMazeSettings = _formatter.Deserialize(_stream) as RazeMazeSettings;
            _stream.Close();

            return _razeMazeSettings;
            
        }
        else
        {

            Debug.Log("No file found at: " + _loadPath);
            return null;

        }

    }

    ///<Summary>
    /// Input _settings as null if you want to revert the saved settings back to the Standard values
    ///</Summary>
    public static void SaveSettings(string _path, RazeMazeSettings _settings)
    {

        if(_settings == null)
        {

            _settings = new RazeMazeSettings(200, 45, false, false);

        }

        string _savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _path);

        BinaryFormatter _formatter = new BinaryFormatter();
        FileStream _stream = new FileStream(_savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        RazeMazeSettings _data = new RazeMazeSettings(_settings.RotationSpeed, _settings.DegreesPerRotate, _settings.SnapTurning, _settings.MovementVignette);

        _formatter.Serialize(_stream, _data);
        _stream.Close();

    }

}
