using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SqueakyToy : MonoBehaviour
{
    Renderer rend;
    public GameObject beast, victim; 
    public Text beastMessage;
    Vector3 spawnPlacement, victimPos, victimDir;   
    public float textTimer = 3, spawnDistance = 6, beastTimer = 5;
    public bool trapTriggered = false;
    public bool timerDown = false;

    private void Start()
    {
        beastMessage.enabled = false;
    }

    private void Update()
    {
        if(timerDown == true)
        {
            TimersDown();
        }

        if (victim != null)
        {
            victimPos = victim.transform.position;
            victimDir = victim.transform.forward;
            Quaternion victimRotation = victim.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        textTimer = 3;
        timerDown = true;
        beastTimer = 5;
        trapTriggered = true;
        victim = other.gameObject;    
        beastMessage.enabled = true;
        rend = gameObject.GetComponentInChildren<Renderer>();
        rend.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    void SpawnBeast()
    {
        if (beastTimer == 0)
        {
            for (int i = 0; i < 1; i++)
            {
                spawnPlacement = victimPos + Vector3.down + victimDir*spawnDistance;
                beast = Instantiate(beast, spawnPlacement, transform.rotation * Quaternion.Euler(0, 180, 0));
                beast.transform.LookAt(victim.transform.position + Vector3.down);
                beastMessage.enabled = false;
                Destroy(gameObject);
            }
        }       
    }

    void TimersDown()
    {
        if (textTimer > 0)
        {
            textTimer -= Time.deltaTime;
        }
        else
        {
            beastMessage.enabled = false;
        }

        if (beastTimer > 0 && trapTriggered == true)
        {
            beastTimer -= Time.deltaTime;
        }
        else if (beastTimer < 0 && trapTriggered == true)
        {
            beastTimer = 0;
            SpawnBeast();
        }
    }
}
