using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHitable : MonoBehaviour
{

    public struct HitterValue
    {
        public int dmg;
        public IHitter hitter;
    }

    public abstract void Hit(HitterValue value);
}
