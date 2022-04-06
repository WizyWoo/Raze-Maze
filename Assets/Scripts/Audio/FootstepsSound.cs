using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    public PlayerMovementController Ref;

    // Update is called once per frame
    void Update()
    {
        if(Ref.grounded == true && Input.GetKeyDown(KeyCode.W))
        {
            PlayFootsteps();
        }
        if(Input.GetKeyUp(KeyCode.W) || Ref.grounded == false)
        {
            PlayOff();
        }

    }
        
    void PlayFootsteps()
    {
        GetComponent<FMODUnity.StudioEventEmitter>().Play();
    }
    void PlayOff()
    {
        GetComponent<FMODUnity.StudioEventEmitter>().Stop();
    }
}
