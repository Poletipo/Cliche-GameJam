using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : BossState
{
    [SerializeField]
    Health _health;
    int _stage = 0 ;
    [SerializeField]
    Animator _animator;
    public float BlockTime = 5;
    float _startBlockTime;
    [SerializeField]
    IdleState _idleState;
    [SerializeField]
    DeadState _deadState;
    [SerializeField]
    Hitable_Weakpoint[] _weakpoints;

    bool _blockingDone = false;
    private bool _isDead;

    private void Start()
    {
        for (int i = 0; i < _weakpoints.Length; i++)
        {
            _weakpoints[i].OnHit += OnHit;
        }
    }

    private void OnHit()
    {
        _stage++;
        switch (_stage)
        {
            case 1:
                _animator.CrossFade("RIG_Boss_01|Boss_Block_Hurt_01", 0.1f);
                _weakpoints[0].Deactivate();
                StartCoroutine(ChangeWeakpoint(1));
                break;
            case 2:
                _weakpoints[1].Deactivate();
                StartCoroutine(ChangeWeakpoint(2));
                _animator.CrossFade("RIG_Boss_01|Boss_Block_Hurt_02", 0.1f);
                break;
            case 3:
                _weakpoints[2].Deactivate();
                StartCoroutine(BlockFinished());
                _animator.CrossFade("RIG_Boss_01|Boss_Block_Hurt_03", 0.1f);
                _health.Hurt(1);

                if(_health.Hp <= 0)
                {
                    _isDead = true;
                }


                break;
        }
    }

    private IEnumerator BlockFinished()
    {
        yield return new WaitForSeconds(1);

        _blockingDone = true;
    }

    public override void EnterState(BossState previousState)
    {
        _weakpoints[0].Activate();
        _animator.CrossFade("RIG_Boss_01|Boss_Block", 0.1f);
        _startBlockTime = Time.time;
    }

    public override BossState UpdateState()
    {

        if (_isDead)
        {
            return _deadState;
        }



        if(_startBlockTime + BlockTime <= Time.time && _stage <3 || _blockingDone)
        {
            return _idleState;
        }

        return null;
    }

    public override void LeaveState()
    {

        for (int i = 0; i < _weakpoints.Length; i++)
        {
            _weakpoints[i].Deactivate();
        }

        _stage = 0;
        _blockingDone = false;
    }

    IEnumerator ChangeWeakpoint(int nextWeakpointIndex)
    {
        yield return new WaitForSeconds(0.5f);
        _weakpoints[nextWeakpointIndex].Activate();
    }



}
