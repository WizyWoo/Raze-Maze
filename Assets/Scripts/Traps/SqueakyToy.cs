using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SqueakyToy : MonoBehaviour
{
    public GameObject beast;
    Vector3 spawnPlacement;
    public Vector3 fwd;
    public Text beastMessage;
    float textTimer = 3;
    public float beastTimer = 5;
    public bool trapTriggered = false;
    Renderer rend;
    GameObject victim;

    private void Start()
    {
        fwd.Set(0, -0.5f, 4);
        rend = gameObject.GetComponentInChildren<Renderer>();
        beastMessage.enabled = false;
    }

    private void Update()
    {
        if (beastTimer > 0 && trapTriggered == true)
        {
            beastTimer -= Time.deltaTime;
        }
        else if (beastTimer == 0)
        {
            SpawnBeast();   
        }

        if (textTimer > 0)
        {
            textTimer -= Time.deltaTime;
        }
        else beastMessage.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        textTimer = 3;
        beastTimer = 5;
        trapTriggered = true;
        victim = other.gameObject;
        beastMessage.enabled = true;        
        rend.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    void SpawnBeast()
    {
        for (int i = 0; i <= 5; i++)
        {
            spawnPlacement = victim.transform.position + fwd;
            beast = Instantiate(beast, spawnPlacement, transform.rotation * Quaternion.Euler(0, 180, 0));
        }
    }
}
