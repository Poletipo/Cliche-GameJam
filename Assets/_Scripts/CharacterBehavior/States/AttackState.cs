using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BossState
{
    [SerializeField]
    Animator _animator;

    [SerializeField]
    Health _health;

    [SerializeField]
    IdleState _idleState;
    [SerializeField]
    BlockState _blockState;

    public int AttackCount = 2;
    int attackCounter = 0;

    public IHitter[] hitters;
    public string[] attackAnimName;


    private int _randomAttack = 0;
    private bool isAttackDone = false;

    private void Start()
    {
        for (int i = 0; i < hitters.Length; i++)
        {
            hitters[i].OnHit += OnTargetHit;
        }

        _health.OnHurt += OnHurt;
    }

    private void OnHurt()
    {
        AttackCount += 3;
    }

    private void OnTargetHit()
    {

        for (int i = 0; i < hitters.Length; i++)
        {
            hitters[i].Deactivate();
        }

        attackCounter--;
        isAttackDone = true;
    }
    public void AnimationOver()
    {
        isAttackDone = true;
    }

    public override void EnterState(BossState previousState)
    {

        _randomAttack = Random.Range(0, attackAnimName.Length);
        _animator.CrossFade(attackAnimName[_randomAttack], 0.2f);
    }

    public override BossState UpdateState()
    {
        if (isAttackDone)
        {
            attackCounter++;
            if(attackCounter >= AttackCount)
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
        isAttackDone = false;
    }
}
