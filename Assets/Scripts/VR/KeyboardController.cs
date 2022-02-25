using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour
{

    public string Output;
    public InputField NameField;
    public TextMesh NameDisplay;

    public void AddLetter(char _letter)
    {

        Output += _letter;
        UpdateName();

    }

    public void RemoveLetter()
    {

        if(Output.Length > 0)
        Output = Output.Remove(Output.Length-1);
        UpdateName();

    }

    public void UpdateName()
    {

        NameField.text = Output;
        NameDisplay.text = Output;

    }

}
