using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnTrigger : MonoBehaviour
{

    public int SceneIndex;

    private void OnTriggerEnter(Collider Other)
    {

        if(Other.transform.root.tag == "Player")
        {

            SceneManager.LoadScene(SceneIndex);

        }

    }

}
