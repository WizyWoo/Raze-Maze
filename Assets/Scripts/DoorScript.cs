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

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Key")
        {
            if (anim != null)
                anim.Play();
        }
    }
}
