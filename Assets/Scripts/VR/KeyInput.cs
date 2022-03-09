using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour , IInteractable
{

    public char Letter;
    private KeyboardController keyboard;

    private void Start()
    {

        keyboard = transform.GetComponentInParent<KeyboardController>();
        gameObject.name = Letter.ToString();

    }

    public void Activate(Transform _player)
    {

        keyboard.AddLetter(Letter);

    }

}