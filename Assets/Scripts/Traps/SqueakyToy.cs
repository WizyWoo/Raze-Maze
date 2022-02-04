using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueakyToy : MonoBehaviour
{
    public GameObject beast;
    Vector3 spawnPlacement;
    public Vector3 fwd;

    private void Start()
    {
        fwd.Set(0, -0.5f, 4);
        spawnPlacement = transform.position + fwd - (Vector3.down) * 0;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        beast = Instantiate(beast, spawnPlacement, transform.rotation * Quaternion.Euler(0, 180, 0));
        Destroy(gameObject); 
    }
}
