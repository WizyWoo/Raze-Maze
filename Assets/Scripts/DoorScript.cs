using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public bool open;
    private Animation anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    public void DoorOpen()
    {
        if (anim != null)
            anim.Play();
    }
}
