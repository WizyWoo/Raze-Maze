using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text FeedbackText, WeaponIDText, HoverText;
    public GameObject playuwu;

    private void Start()
    {

        playuwu = GameObject.FindGameObjectWithTag("Player");
        playuwu.GetComponentInChildren<PlayerWeaponController>().FeedbackText = FeedbackText;
        playuwu.GetComponentInChildren<PlayerWeaponController>().WeaponIDText = WeaponIDText;
        playuwu.GetComponentInChildren<PlayerInteraction>().HoverText = HoverText;

    }

}
