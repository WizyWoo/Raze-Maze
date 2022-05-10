using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject blockingWall, tutorialText;

    private void Start() 
    {
        if(blockingWall != null && tutorialText != null)
        {
          blockingWall.SetActive(true);
          tutorialText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" && tutorialText != null)
            tutorialText.SetActive(true);
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {   
            if(blockingWall != null && tutorialText != null)
            {
               blockingWall.SetActive(false);
               tutorialText.SetActive(false);
            }
        }
    }
}
