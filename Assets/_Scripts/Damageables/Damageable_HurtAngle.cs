using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable_HurtAngle : IDamageable {
    public Health Hp;
    public Flash flash;
    public MovementController _mc;
    public AudioClip shoveSFX;
    public float frontAngle = 90;

    public override bool Hit(HitterValue value) {
        bool isHurt = false;

        float dotTest = Mathf.Cos(frontAngle * Mathf.Deg2Rad);

        Vector3 knockBackDirection = (transform.position - value.hitter.transform.position);
        knockBackDirection.y = 0;
        knockBackDirection.Normalize();

        float dotValue = Vector3.Dot(transform.forward, -knockBackDirection);

        if (dotValue >= dotTest) {
            isHurt = Hp.Hurt(value.dmg);
            if (isHurt) {
                flash.StartFlash();
            }
        }


        if (isHurt) {
            _mc.KnockBack(knockBackDirection, value.force, value.knockTime);
        }
        else {
            AudioManager.Instance.PlayAudio(shoveSFX, transform.position);
            _mc.KnockBack(knockBackDirection, (value.force / 2), value.knockTime);
        }

        return isHurt;
    }
}
