using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable_KnockBack : IHitable
{
    public float knockBackForce = 50;

    public override bool Hit(HitterValue value)
    {

        Vector3 direction = value.hitter.HitterSource.transform.position - transform.position ;

        //value.hitter.HitterSource.GetComponentInParent<MovementController>().KnockBack(direction);


        return true;
    }
}
