using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    public float InteractionDistance;
    public Text HoverText;
    private LayerMask interactMask;

    private void Start()
    {

        interactMask = ~LayerMask.NameToLayer("Interactables");
        
    }

    private void Update()
    {

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, InteractionDistance, interactMask))
        {

            HoverText.text = hit.transform.name;

        }
        else
        {

            HoverText.text = "";

        }

        if(Input.GetKeyDown(KeyCode.E) && hit.transform.TryGetComponent<IPickup>(out IPickup _pickup))
        {

            _pickup.Interact();

        }

    }

}
