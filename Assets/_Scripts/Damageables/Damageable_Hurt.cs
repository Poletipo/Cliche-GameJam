using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable_Hurt : IDamageable {
    public Health Hp;
    public Flash Flash;
    public MovementController Mc;

    public override bool Hit(HitterValue value) {
        bool isHurt = Hp.Hurt(value.dmg);
        if (isHurt) {
            Flash.StartFlash();

            Vector3 knockBackDirection = (transform.position - value.hitter.transform.position);
            knockBackDirection.y = 0;
            knockBackDirection.Normalize();

            Mc.KnockBack(knockBackDirection, value.force, value.knockTime);

            return true;
        }
        return false;
    }
}
