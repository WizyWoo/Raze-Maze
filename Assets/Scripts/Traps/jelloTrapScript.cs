using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelloTrapScript : TrapController
{
    //private float jelloMultiplier = 1f;

    //private void OnTriggerStay(Collider other)
    //{
    //    jelloMultiplier = 0.5f;

    //    other.GetComponent<PlayerController>()._speed *= jelloMultiplier;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    jelloMultiplier = 2f;

    //    other.GetComponent<PlayerController>()._speed *= jelloMultiplier;
    //}

    bool started;
    public LayerMask m_LayerMask;

    List<Collider> colliderList = new List<Collider>();

    void FixedUpdate()
    {
        CollisionsChecker();
    }

    void CollisionsChecker()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        int i = 0;

        foreach (var item in hitColliders)
        {
            item.gameObject.GetComponent<PlayerController>()._speedMultiplier = 0.5f;

            //for (int j = 0; j < length; j++)
            //{

            //}
        }

        //Check when there is a new collider coming into contact with the box
        //while (i < hitColliders.Length)
        //{
            
        //    //Increase the number of Colliders in the array
        //    i++;
        //}
    }
}
