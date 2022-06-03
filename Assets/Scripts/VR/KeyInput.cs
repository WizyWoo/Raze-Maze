using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour , IInteractable
{

    public char Letter;
    public TextMesh KeyTextMesh;
    private KeyboardController keyboard;

    private void Start()
    {

        keyboard = transform.GetComponentInParent<KeyboardController>();
        Letter = (char)gameObject.name[0];
        KeyTextMesh.text = Letter.ToString();

    }

    public void Activate(Transform _player)
    {

        keyboard.AddLetter(Letter);

    }

}
