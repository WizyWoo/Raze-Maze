using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinItemsCheckTemp : MonoBehaviour
{

    public GameObject RatTrophy;

    private void Start()
    {

        RatTrophy.SetActive(LocalGameController.main.WinCheck());

    }

}
