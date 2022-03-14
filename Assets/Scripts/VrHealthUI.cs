using Com.MyCompany.MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VrHealthUI : MonoBehaviour
{
    [SerializeField]
    private PlayerManager target;

    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    private Text playerNameText, playerLives;


    [Tooltip("UI Slider to display Player's Health")]
    [SerializeField]
    private Slider playerHealthSlider;

    private void Start()
    {
        SetName();
    }

    void Update()
    {
        // Reflect the Player Health
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = target.Health;
            playerLives.text = target.lives.ToString();
        }
    }

    public void SetName()
    {
        if (playerNameText != null)
        {
            playerNameText.text = target.photonView.Owner.NickName;
        }
    }
}

