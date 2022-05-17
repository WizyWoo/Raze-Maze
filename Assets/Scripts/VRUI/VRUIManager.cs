using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRUIManager : MonoBehaviour
{

    public InputAction MenuToggle, SliderControl;
    public GameObject VRMenu;
    private bool menuOpen;

    private void OnEnable()
    {

        MenuToggle.Enable();
        SliderControl.Enable();

    }

    private void OnDisable()
    {

        MenuToggle.Disable();
        SliderControl.Disable();

    }

    private void Update()
    {

        if(MenuToggle.WasPressedThisFrame())
        {

            menuOpen = !menuOpen;
            VRMenu.SetActive(menuOpen);

        }

    }

}
