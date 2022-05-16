using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RazeMazeSettings
{

    public int SettingFileVersion;
    public float RotationSpeed;
    public int DegreesPerRotate;
    public bool SnapTurning, MovementVignette;

    public RazeMazeSettings(float _rotationSpeed, int _degreesPerRotate, bool _snapTurning, bool _movementVignette)
    {
        
        RotationSpeed = _rotationSpeed;
        DegreesPerRotate = _degreesPerRotate;
        SnapTurning = _snapTurning;
        MovementVignette = _movementVignette;

    }

}