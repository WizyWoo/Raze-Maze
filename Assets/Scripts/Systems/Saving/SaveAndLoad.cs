using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveAndLoad : MonoBehaviour
{

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

    public static void SaveSettings(string _path, float _rotationSpeed = 200, int _degreesPerRotate = 45, bool _snapTurning = false, bool _movementVignette = false)
    {

        string _savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _path);

        BinaryFormatter _formatter = new BinaryFormatter();
        FileStream _stream = new FileStream(_savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        RazeMazeSettings _data = new RazeMazeSettings(_rotationSpeed, _degreesPerRotate, _snapTurning, _movementVignette);

        _formatter.Serialize(_stream, _data);
        _stream.Close();

    }

}
