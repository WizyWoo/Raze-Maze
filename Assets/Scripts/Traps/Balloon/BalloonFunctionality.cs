using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFunctionality : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    private Vector3 startPos;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        GetComponentInChildren<Rigidbody>().isKinematic = true;
        GetComponentInChildren<Rigidbody>().transform.localPosition = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer < 5){
            GetComponentInChildren<Rigidbody>().transform.localPosition = new Vector3(0,0,0);
        }else{
            Destroy(gameObject);
        }
        if(positions.Count < 5){
            int length = positions.Count;
            for (int i = length - 1; i < 6 - length; i++)
            {
                positions.Add(positions[i-1]);    
            }        
        }
        timer += Time.fixedDeltaTime;
        switch (timer)
        {
            case < 1:
            transform.position = Vector3.Lerp(startPos, positions[positions.Count - 1], timer);
            break;
            case < 2:
            transform.position = Vector3.Lerp(positions[positions.Count - 1], positions[positions.Count - 2], timer - 1);
            break;
            case < 3:
            transform.position = Vector3.Lerp(positions[positions.Count - 2], positions[positions.Count - 3], timer - 2);
            break;
            case < 4:
            transform.position = Vector3.Lerp(positions[positions.Count - 3], positions[positions.Count - 4], timer - 3);
            break;
            case < 5:
            transform.position = Vector3.Lerp(positions[positions.Count - 4], positions[positions.Count - 5], timer - 4);
            break;
            case > 5:
            GetComponentInChildren<Rigidbody>().isKinematic = false;
            GetComponentInChildren<PlayerPositionLog>().transform.parent = null;
            Destroy(gameObject);
            break;
        }
    }
}
