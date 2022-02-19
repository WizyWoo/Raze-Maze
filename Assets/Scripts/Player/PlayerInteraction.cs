using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    public float interactionDistance;
    public bool DesktopMode;
    public Text HoverText;
    public TextMesh HoverTextMesh;
    private LayerMask interactMask;

    private void Start()
    {

        interactMask = 1 << LayerMask.NameToLayer("Interactables");

        if(DesktopMode)
            UpdateUIRefs();
        
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

            if(DesktopMode)
            {

                if(HoverText)
                    HoverText.text = "^ " + hit.transform.name + " ^";
                else
                    UpdateUIRefs();

            }
            else
            {

                HoverTextMesh.text = "^ " + hit.transform.name + " ^";

            }

        }
        else
        {

            if(DesktopMode)
            {

                if(HoverText)
                    HoverText.text = "";

            }
            else
            {

                if(HoverTextMesh)
                    HoverTextMesh.text = "";

            }

        }

    }

}
