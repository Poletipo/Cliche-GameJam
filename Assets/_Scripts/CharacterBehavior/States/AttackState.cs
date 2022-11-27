using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BossState
{
    [SerializeField]
    Animator _animator;

    public int attackCount = 2;
    int attackCounter = 0;


    public IHitter[] hitters;

    public float AttackTime = 3;
    float _startAttackTime;
    [SerializeField]
    IdleState _idleState;
    [SerializeField]
    BlockState _blockState;

    public override void EnterState(BossState previousState)
    {
        Debug.Log("aTTACK");
        _animator.CrossFade("RIG_Boss_01|Boss_Attack_01", 0.2f);

        for (int i = 0; i < hitters.Length; i++)
        {
            hitters[i].Activate();
        }

        _startAttackTime = Time.time;
    }

    public override BossState UpdateState()
    {
        if (_startAttackTime + AttackTime <= Time.time)
        {

            attackCounter++;
            if(attackCounter >= attackCount)
            {
                attackCounter = 0;
                return _blockState;
            }

            return _idleState;
        }

        return null;
    }

    public override void LeaveState()
    {
        for (int i = 0; i < hitters.Length; i++)
        {
            hitters[i].Deactivate();
        }
    }
}
