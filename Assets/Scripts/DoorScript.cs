using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour, IInteractable
{
    public static bool doorKey;
    private bool open, close, inTrigger;

    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    void Activate(Transform _player)
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
        }

        if (open)
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
        else
        {
            var newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
    }
}
