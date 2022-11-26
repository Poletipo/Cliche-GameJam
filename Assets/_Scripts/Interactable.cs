using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public abstract bool Interact(PlayerController player);
    public abstract void ShowUI();
    public abstract void HideUI();


}
