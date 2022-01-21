using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFunctionality : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    private Vector3 startPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(positions.Count > 5){
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
                case < 6:
                GetComponentInChildren<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePosition;
                GetComponentInChildren<PlayerPositionLog>().transform.parent = null;
                Destroy(gameObject);
                break;
            }
        }
    }
}
