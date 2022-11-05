using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable_Hurt : IHitable
{
    public Health Hp;

    public override void Hit(HitterValue value)
    {
        Hp.Hurt(value.dmg);
    }
}
