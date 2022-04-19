using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColliders : MonoBehaviour
{

    public Collider[] CollidersToIgnore;
    public Collider MyCol;

    private void Start()
    {

        foreach(Collider _col in CollidersToIgnore)
        {

            Physics.IgnoreCollision(_col, MyCol, true);

        }

    }

}
