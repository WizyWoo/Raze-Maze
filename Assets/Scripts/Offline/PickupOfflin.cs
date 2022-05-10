using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOfflin : MonoBehaviour , IInteractable
{

    public GameObject Thing;

    public void Activate(Transform _player)
    {

        Thing.SetActive(true);

    }

}
