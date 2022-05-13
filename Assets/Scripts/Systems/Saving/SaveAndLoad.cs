using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveAndLoad : MonoBehaviour
{

    public string FileName;

    public RazeMazeSettings LoadSettings()
    {

        string _savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FileName);

        if(File.Exists(_savePath))
        {

            RazeMazeSettings _razeMazeSettings = null;

            BinaryFormatter _formatter = new BinaryFormatter();
            FileStream _stream = new FileStream(_savePath, FileMode.OpenOrCreate, FileAccess.Read);

            _razeMazeSettings = _formatter.Deserialize(_stream) as RazeMazeSettings;

            _stream.Close();

            return _razeMazeSettings;
            
        }
        else
        {

            Debug.Log("No file found at: " + _savePath);
            return null;

        }

    }

    public void SaveSettings(bool _snapTurning, bool _movementVignette)
    {

        string _savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FileName);

        BinaryFormatter _formatter = new BinaryFormatter();
        FileStream _stream = new FileStream(_savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        RazeMazeSettings _data = new RazeMazeSettings(_snapTurning, _movementVignette);

        _formatter.Serialize(_stream, _data);

        _stream.Close();

    }

}
