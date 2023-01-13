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
            if(Interactable != null)
            {
                Interactable.HideUI();
            }

            Interactable = interact;
            InteractInRange = true;

            Interactable.ShowUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interact = other.GetComponent<Interactable>();

        if(InteractInRange && interact == Interactable)
        {
            Interactable.HideUI();
            Interactable = null;
            InteractInRange = false;
        }
    }


}
