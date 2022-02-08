using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeMine : MonoBehaviour
{
    ParticleSystem ps;
    
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Pause();
    }

   
    
    
    void OnCollisionEnter(Collision other)
    {
        ps.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        ps.Play();
    }
}
