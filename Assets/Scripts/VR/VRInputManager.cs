using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRInputManager : MonoBehaviour
{

    public enum Hand
    {

        Right,
        Left

    }

    public Hand HandLR;
    
    public VRPlayerInputs PlayerInputs;
    public InputAction GrabButton, TriggerButton;

    private void Awake()
    {

        PlayerInputs = new VRPlayerInputs();

        switch(HandLR)
        {

            case Hand.Right:
            GrabButton = PlayerInputs.Player.GrabRight;
            TriggerButton = PlayerInputs.Player.FireRight;
            break;

            case Hand.Left:
            GrabButton = PlayerInputs.Player.GrabLeft;
            TriggerButton = PlayerInputs.Player.FireLeft;
            break;

        }

    }

    private void OnEnable()
    {

        GrabButton.Enable();
        TriggerButton.Enable();

    }

    private void OnDisable()
    {

        GrabButton.Disable();
        TriggerButton.Disable();

    }

}
