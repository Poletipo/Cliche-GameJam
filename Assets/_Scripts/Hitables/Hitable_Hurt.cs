using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable_Hurt : IHitable
{
    public Health Hp;
    public Flash flash;
    public MovementController _mc;

    public override void Hit(HitterValue value)
    {
        bool isHurt = Hp.Hurt(value.dmg);
        if (isHurt)
        {
            flash.StartFlash();

            Vector3 knockBackDirection = (transform.position - value.hitter.transform.position);
            knockBackDirection.y = 0;
            knockBackDirection.Normalize();

            _mc.KnockBack(knockBackDirection, value.force,value.knockTime);
        }
    }
}
