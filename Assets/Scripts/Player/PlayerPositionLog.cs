using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionLog : MonoBehaviour
{
    public List<Vector3> playerPosHistory = new List<Vector3>();
    void Start()
    {
        InvokeRepeating("LogPos", 0f, 1f);
    }
    void LogPos(){
        playerPosHistory.Add(transform.position);
    }
}
