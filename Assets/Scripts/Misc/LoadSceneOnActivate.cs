using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnActivate : MonoBehaviour , IInteractable
{

    public int SceneIndex;

    public void Activate(Transform _player)
    {

        SceneManager.LoadScene(SceneIndex);

    }

}
