using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BossState
{

    [SerializeField]
    Animator _animator;



    public override void EnterState(BossState previousState)
    {
        Debug.Log("ImDead, oh no. The pain, the misery...");
        _animator.CrossFade("RIG_Boss_01|Boss_Death", .2f);
    }

    public override void LeaveState()
    {
    }

    public override BossState UpdateState()
    {
        return null;
    }
}
