using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable_KnockBack : IDamageable {
    public float KnockBackForce = 50;

    public override bool Hit(HitterValue value) {

        Vector3 direction = value.hitter.HitterSource.transform.position - transform.position;

        //value.hitter.HitterSource.GetComponentInParent<MovementController>().KnockBack(direction);


        return true;
    }
}
