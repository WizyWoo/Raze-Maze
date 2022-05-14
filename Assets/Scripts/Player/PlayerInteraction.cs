using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : VRInputManager
{

    public float interactionDistance;
    public bool DesktopMode;
    public Text HoverText;
    public TextMesh HoverTextMesh;
    private LayerMask interactMask;
    private LineRenderer laserPointer;

    private void Start()
    {

        interactMask = ((1 << LayerMask.NameToLayer("Interactables")) + (1 << LayerMask.NameToLayer("Default")));

        if(DesktopMode)
            UpdateUIRefs();

        laserPointer = gameObject.GetComponent<LineRenderer>();
        
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

            if(hit.transform.TryGetComponent<IInteractable>(out IInteractable _inter))
            {

                if((Input.GetKeyDown(KeyCode.E) || TriggerButton.WasPressedThisFrame()) && hit.transform.TryGetComponent<IInteractable>(out IInteractable _IInteractable))
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

                    laserPointer.SetPosition(0, transform.position);
                    laserPointer.SetPosition(1, hit.point);

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

                    laserPointer.SetPosition(0, transform.position);
                    laserPointer.SetPosition(1, transform.position);

                }

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

                laserPointer.SetPosition(0, transform.position);
                laserPointer.SetPosition(1, transform.position);

            }

        }

    }

}
