using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable_KnockBack : IHitable
{
    public float knockBackForce = 50;

    public override void Hit(HitterValue value)
    {

        Vector3 direction = value.hitter.HitterSource.transform.position - transform.position ;

        value.hitter.HitterSource.GetComponent<MovementController>().KnockBack(direction, knockBackForce);


    }
}
