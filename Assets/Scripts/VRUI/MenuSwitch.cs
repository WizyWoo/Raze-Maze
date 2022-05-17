using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour , IInteractable
{

    public GameObject ThisMenu, OtherMenu;

    public void Activate(Transform _player)
    {

        OtherMenu.SetActive(true);
        ThisMenu.SetActive(false);

    }

}
