using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelloTrapScript : TrapController
{
    //private float jelloMultiplier = 1f;
    private float movSpeed;

    private void OnTriggerEnter(Collider other)
    {
        movSpeed =/* other.transform.root.GetComponent<VrPlayerController>().MovementSpeed;*/ 8f;
        //jelloMultiplier = 0.1f;

        //other.GetComponent<PlayerController>()._speedMultiplier = jelloMultiplier;

        other.transform.root.GetComponent<VrPlayerController>().MovementSpeed = movSpeed / 3f;
    }

    private void OnTriggerExit(Collider other)
    {
        //jelloMultiplier = 1f;

        //other.GetComponent<PlayerController>()._speedMultiplier = jelloMultiplier;

        other.transform.root.GetComponent<VrPlayerController>().MovementSpeed = movSpeed;
    }

    //bool started;
    //public LayerMask m_LayerMask;

    //List<Collider> colliderList = new List<Collider>();

    //void FixedUpdate()
    //{
    //    CollisionsChecker();
    //}

    //void CollisionsChecker()
    //{
    //    //Use the OverlapBox to detect if there are any other colliders within this box area.
    //    //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
    //    Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);

    //    foreach (var item in hitColliders)
    //    {
    //        if(colliderList.Count > 0){
    //            bool match = false;
    //            foreach (var cols in colliderList)
    //            {
    //                if(cols == item){
    //                    match = true;
    //                }
    //            }
    //            if(!match){
    //                colliderList.Add(item);
    //            }
    //        }else{
    //            colliderList.Add(item);
    //        }
    //    }
    //    if(colliderList.Count > 0){
    //        foreach (var playerList in colliderList)
    //        {
    //            bool inTrigger = false;
    //            foreach (var colliderList in hitColliders)
    //            {
    //                if(playerList == colliderList){
    //                    inTrigger = true;
    //                }
    //            }
    //            if(!inTrigger){
    //                playerList.gameObject.GetComponent<PlayerController>()._speedMultiplier = 1f;
    //            }else{
    //                playerList.gameObject.GetComponent<PlayerController>()._speedMultiplier = 0.5f;
    //            }
    //        }
    //    }

    //    //Check when there is a new collider coming into contact with the box
    //    //while (i < hitColliders.Length)
    //    //{

    //    //    //Increase the number of Colliders in the array
    //    //    i++;
    //    //}
}

