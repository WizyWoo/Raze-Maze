using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveKey : MonoBehaviour , IInteractable
{

    private KeyboardController keyboard;

    private void Start()
    {

        keyboard = transform.GetComponentInParent<KeyboardController>();

    }

    public void Activate(Transform _player)
    {

        keyboard.RemoveLetter();

    }

}
