using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggle : MonoBehaviour , IInteractable
{

    public VRUIManager VRUI;

    public void Activate(Transform _player)
    {

        VRUI.ToggleUIMode();

    }

}
