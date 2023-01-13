using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable {
    enum DoorState {
        Locked,
        Unlocked,
        Open
    }

    public AnimationCurve UnlockAnimCurve;
    public GameObject InteractUI;
    public AudioClip UnlockSFX;
    public AudioClip LockedSFX;
    public Animation Anim;
    public float UnlockTime = 1;

    private DoorState _currentState = DoorState.Locked;
    private float _unlockStartTime;


    public void UnlockDoor(PlayerController player) {
        AudioManager.Instance.PlayAudio(UnlockSFX, transform.position);
        Anim.Play("DoorUI_Unlock_Accepted");
        _unlockStartTime = Time.time;
        _currentState = DoorState.Unlocked;
    }

    private void DeniedUnlock() {
        Anim.Play("DoorUI_Unlock_Denied");
        AudioManager.Instance.PlayAudio(LockedSFX, transform.position);
    }

    public override bool Interact(PlayerController player) {
        if (_currentState == DoorState.Locked && player.Keycount > 0) {
            player.Keycount--;
            UnlockDoor(player);
            return true;
        }

        DeniedUnlock();

        return false;
    }

    public void Orientation(float angle) {
        transform.rotation = Quaternion.Euler(Vector3.up * angle);
    }

    private void Update() {
        switch (_currentState) {
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

    private void HandleUnlockState() {
        float t = (Time.time - _unlockStartTime) / UnlockTime;

        if (t >= 1) {
            GetComponent<Collider>().enabled = false;
            _currentState = DoorState.Open;
        }

        Vector3 pos = transform.position;
        pos.y = UnlockAnimCurve.Evaluate(t);
        transform.position = pos;

    }

    public override void ShowUI() {
        InteractUI.SetActive(true);
    }

    public override void HideUI() {
        InteractUI.SetActive(false);
    }
}
