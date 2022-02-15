using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.MyCompany.MyGame;

public class UIManager : MonoBehaviour
{

    public Text FeedbackText, WeaponIDText, HoverText;
    public GameObject playuwu;

    private void Start()
    {

        /*GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in _players)
        {

            if(p == PlayerManager.LocalPlayerInstance)
                playuwu = p;
            
        }
        playuwu.GetComponentInChildren<PlayerWeaponController>().FeedbackText = FeedbackText;
        playuwu.GetComponentInChildren<PlayerWeaponController>().WeaponIDText = WeaponIDText;
        playuwu.GetComponentInChildren<PlayerInteraction>().HoverText = HoverText;*/

    }

}
