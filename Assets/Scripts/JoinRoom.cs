using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyCompany.MyGame;

public class JoinRoom : MonoBehaviour , IInteractable
{

    public MyLauncher LauncherScript;

    public void Activate(Transform _player)
    {

        LauncherScript.Connect();

    }

}
