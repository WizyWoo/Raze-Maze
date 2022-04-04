using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour, IInteractable
{
    public static bool doorKey = false;
    public bool keycheck;
    public bool open, close = true, inTrigger;
    private Animation anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        keycheck = doorKey;
    }

    void Update()
    {
        keycheck = doorKey;
    }

    void OnTriggerEnter(Collider other)
    {
        //TODO: check if it's the player
        if(other.CompareTag("Player"))
            inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            inTrigger = false;
    }

    public void Activate(Transform _player)
    {
        if (inTrigger)
        {
            if (close)
            {
                if (doorKey)
                {
                    open = true;                   
                    close = false;
                }
            }
            else
            {
                close = true;
                open = false;                
            }

            if (open)
            {
                if (anim != null)
                    anim.Play();
            }                  
        }
    }
}
