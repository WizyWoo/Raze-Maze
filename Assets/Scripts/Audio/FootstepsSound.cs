using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            PlayFootsteps();
        }
        if(Input.GetKeyUp(KeyCode.W))
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
