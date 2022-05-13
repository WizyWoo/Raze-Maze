using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RazeMazeSettings
{

    public int SettingFileVersion;
    public float RotationSpeed;
    public bool SnapTurning, MovementVignette;

    public RazeMazeSettings(float _rotationSpeed, bool _snapTurning = false, bool _movementVignette = false)
    {
        
        RotationSpeed = _rotationSpeed;
        SnapTurning = _snapTurning;
        MovementVignette = _movementVignette;

    }

}