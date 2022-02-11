using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField]
    public float interactionDistance;
    public Text HoverText;
    private LayerMask interactMask;

    private void Start()
    {

        interactMask = 1 << LayerMask.NameToLayer("Interactables");
        
    }

    public void UpdateUIRefs()
    {

        GameObject temp = GameObject.FindGameObjectWithTag("Hud");
        Text[] tempTexts = temp.GetComponentsInChildren<Text>();

        for(int i = 0; i < tempTexts.Length; i++)
        {

            if(tempTexts[i].name == "HoverText")
            {

                HoverText = tempTexts[i];

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

            HoverText.text = "^ " + hit.transform.name + " ^";

        }
        else
        {

            HoverText.text = "";

        }

    }

}
