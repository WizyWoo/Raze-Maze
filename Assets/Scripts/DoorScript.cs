using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour, IInteractable
{
    public static bool doorKey = false;
    public bool keycheck;
    private bool open, close = true, inTrigger;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        keycheck = doorKey;
    }

    void Update()
    {
        keycheck = doorKey;
    }

    void OnTriggerEnter(Collider other)
    {
        //TODO: check if it's the player
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
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
                    anim.Play("doorOpening");
            }                  
        }
    }
}
