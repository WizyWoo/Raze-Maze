using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public bool open;
    public Animation anim;

    public void DoorOpen()
    {
        if (anim != null)
            anim.Play();
     
        open = true;
    }
}
