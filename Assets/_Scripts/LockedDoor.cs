using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable
{
    enum DoorState
    {
        Locked,
        Unlocked,
        Open
    }

    public AnimationCurve unlockAnimCurve;
    public float UnlockTime = 1;
    public GameObject InteractUI;
    public Animation anim;
    private float UnlockStartTime;

    DoorState currentState = DoorState.Locked;

    public void UnlockDoor(PlayerController player)
    {
        //TODO: anim for interact UI
        anim.Play("DoorUI_Unlock_Accepted");
        UnlockStartTime = Time.time;
        currentState = DoorState.Unlocked;
    }

    private void DeniedUnlock()
    {
        anim.Play("DoorUI_Unlock_Denied");
    }

    public override bool Interact(PlayerController player)
    {
        if(currentState == DoorState.Locked && player.Keycount > 0)
        {
            player.Keycount--;
            UnlockDoor(player);
            return true;
        }

        DeniedUnlock();

        return false;
    }

    public void Orientation(float angle)
    {
        transform.rotation = Quaternion.Euler(Vector3.up * angle);
    }

    private void Update()
    {
        switch (currentState)
        {
            case DoorState.Locked:
                break;
            case DoorState.Unlocked:
                HandleUnlockState();
                break;
            case DoorState.Open:
                break;
            default:
                break;
        }
    }

    private void HandleUnlockState()
    {
        float t = (Time.time - UnlockStartTime) / UnlockTime;

        if(t >= 1)
        {
            GetComponent<Collider>().enabled = false;
            currentState = DoorState.Open;
        }

        Vector3 pos = transform.position;
        pos.y = unlockAnimCurve.Evaluate(t);
        transform.position = pos;
        
    }

    public override void ShowUI()
    {
        InteractUI.SetActive(true);
    }

    public override void HideUI()
    {
        InteractUI.SetActive(false);
    }
}
