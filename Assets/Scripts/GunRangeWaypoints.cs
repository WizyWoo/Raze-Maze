using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRangeWaypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWP = 0;
    private float wPRadius = 1;
    public float speed;

    void Update()
    {
        if(Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < wPRadius)
        {
            currentWP++;

            if(currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWP].transform.position, Time.deltaTime * speed);
    }
}
