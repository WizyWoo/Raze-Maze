using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField]
    public float interactionDistance;
    [SerializeField]
    private Text hoverText;
    private LayerMask interactMask;

    private void Start()
    {

        interactMask = ~LayerMask.NameToLayer("Interactables");
        
    }

    private void Update()
    {

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactMask))
        {

            hoverText.text = hit.transform.name;

            if(Input.GetKeyDown(KeyCode.E) && hit.transform.TryGetComponent<IInteractable>(out IInteractable _IInteractable))
            {

                _IInteractable.Activate(transform);

            }

        }
        else
        {

            hoverText.text = "";

        }

    }

}
