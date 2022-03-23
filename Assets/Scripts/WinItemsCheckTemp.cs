using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinItemsCheckTemp : MonoBehaviour
{

    public GameObject WinItems;

    private void Start()
    {

        WinItems.SetActive(LocalGameController.main.WinCheck());

    }

}
