using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameController : MonoBehaviour
{

    public static LocalGameController main { get; private set; }

    private void Awake()
    {

        if(LocalGameController.main != this)
            Destroy(gameObject);
        else
            main = this;

    }

}
