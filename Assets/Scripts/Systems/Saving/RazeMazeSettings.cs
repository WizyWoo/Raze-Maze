using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazeMazeSettings
{

    public bool SnapTurning, MovementVignette;

    public RazeMazeSettings(bool _snapTurning = false, bool _movementVignette = false)
    {

        SnapTurning = _snapTurning;
        MovementVignette = _movementVignette;

    }

}