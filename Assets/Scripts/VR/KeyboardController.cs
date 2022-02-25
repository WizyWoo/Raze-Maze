using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour
{

    public string Output;
    public InputField NameField;

    public void AddLetter(char _letter)
    {

        Output += _letter;

        NameField.text = Output;

    }

}
