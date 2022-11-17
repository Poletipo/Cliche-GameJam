using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    public bool InteractInRange = false;
    public Interactable Interactable { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interact = other.GetComponent<Interactable>();

        if(interact != null)
        {
            Interactable = interact;
            InteractInRange = true;
            Debug.Log(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interact = other.GetComponent<Interactable>();

        if(InteractInRange && interact == Interactable)
        {
            Interactable = null;
            InteractInRange = false;
            Debug.Log(other.name);
        }
    }


}
