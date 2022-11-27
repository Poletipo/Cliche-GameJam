using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BossState
{
    [SerializeField]
    Animator _animator;
    public float IdleTime = 1;
    float _startIdleTime;
    [SerializeField]
    AttackState _attackState;

    private void Start()
    {

    }

    public override void EnterState(BossState previousState)
    {
        Debug.Log("Idle");
        _animator.CrossFade("RIG_Boss_01|Boss_Idle", 0.2f);
        _startIdleTime = Time.time;
    }

    public override BossState UpdateState()
    {

        if(_startIdleTime + IdleTime <= Time.time)
        {
            return _attackState;
        }


        return null;
    }

    public override void LeaveState() { }
}
