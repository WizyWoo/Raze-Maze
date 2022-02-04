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

        interactMask = 1 << LayerMask.NameToLayer("Interactables");

        GameObject temp = GameObject.FindGameObjectWithTag("Hud");
        Text[] tempTexts = temp.GetComponentsInChildren<Text>();

        for(int i = 0; i < tempTexts.Length - 1; i++)
        {

            if(tempTexts[i].name == "HoverText")
            {

                hoverText = tempTexts[i];

            }

        }
        
    }

    private void Update()
    {

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactMask))
        {

            if(Input.GetKeyDown(KeyCode.E) && hit.transform.TryGetComponent<IInteractable>(out IInteractable _IInteractable))
            {

                _IInteractable.Activate(transform);

            }

            hoverText.text = "^ " + hit.transform.name + " ^";

        }
        else
        {

            hoverText.text = "";

        }

    }

}
