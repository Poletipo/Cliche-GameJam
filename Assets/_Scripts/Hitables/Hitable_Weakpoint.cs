using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable_Weakpoint : IHitable
{
    public Flash flash;

    bool isActivated = false;


    public override void Hit(HitterValue value)
    {

        if (isActivated)
        {
            OnHit?.Invoke();
            flash.StartFlash();
        }

    }

    public new void Activate()
    {
        isActivated = true;
    }

    public new void Deactivate()
    {
        isActivated = false;
    }

}
