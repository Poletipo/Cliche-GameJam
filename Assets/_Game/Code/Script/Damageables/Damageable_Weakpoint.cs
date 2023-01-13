using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable_Weakpoint : IDamageable {
    public Flash flash;
    [SerializeField] AudioClip hurtSFX;

    private bool isActivated = false;

    public override bool Hit(HitterValue value) {

        if (isActivated) {
            OnHit?.Invoke();
            flash.StartFlash();
            AudioManager.Instance.PlayAudio(hurtSFX, transform.position, 50);

            return true;
        }
        return false;

    }

    public new void Activate() {
        isActivated = true;
    }

    public new void Deactivate() {
        isActivated = false;
    }

}
