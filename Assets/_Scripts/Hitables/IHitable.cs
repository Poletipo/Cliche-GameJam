using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHitable : MonoBehaviour
{

    public Action OnHit;

    public struct HitterValue
    {
        public int dmg;
        public IHitter hitter;
        public float force;
        public float knockTime;
    }

    public abstract void Hit(HitterValue value);


    public void Activate() { }
    public void Deactivate() { }

}
