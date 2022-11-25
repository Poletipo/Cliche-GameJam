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
    private float UnlockStartTime;

    DoorState currentState = DoorState.Locked;

    public void UnlockDoor(PlayerController player)
    {
        UnlockStartTime = Time.time;
        currentState = DoorState.Unlocked;
    }

    public override bool Interact(PlayerController player)
    {
        if(currentState == DoorState.Locked && player.Keycount > 0)
        {
            player.Keycount--;
            UnlockDoor(player);
            return true;
        }

        return false;
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
}
