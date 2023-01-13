using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : BossState {

    [SerializeField] Health _health;
    [SerializeField] Animator _animator;
    [SerializeField] IdleState _idleState;
    [SerializeField] DeadState _deadState;
    [SerializeField] Damageable_Weakpoint[] _weakpoints;
    [SerializeField] float BlockTime = 5;

    private bool _blockingDone = false;
    private bool _isDead;
    private float _startBlockTime;
    private int _stage = 0;

    private void Start() {
        for (int i = 0; i < _weakpoints.Length; i++) {
            _weakpoints[i].OnHit += OnHit;
        }
    }

    private void OnHit() {
        _stage++;
        switch (_stage) {
            case 1:
                StartCoroutine(ChangeWeakpoint(1));
                break;
            case 2:
                StartCoroutine(ChangeWeakpoint(2));
                break;
            case 3:
                StartCoroutine(BlockFinished());
                break;
        }
    }

    private IEnumerator BlockFinished() {

        _weakpoints[2].Deactivate();
        _animator.CrossFade("RIG_Boss_01|Boss_Block_Hurt_03", 0.1f);
        _health.Hurt(1);

        if (_health.Hp <= 0) {
            _isDead = true;
        }

        yield return new WaitForSeconds(1);

        _blockingDone = true;
    }

    public override void EnterState(BossState previousState) {
        _weakpoints[0].Activate();
        _animator.CrossFade("RIG_Boss_01|Boss_Block", 0.1f);
        _startBlockTime = Time.time;
    }

    public override BossState UpdateState() {

        if (_isDead) {
            return _deadState;
        }

        if (_startBlockTime + BlockTime <= Time.time && _stage < 3 || _blockingDone) {
            return _idleState;
        }

        return null;
    }

    public override void LeaveState() {

        for (int i = 0; i < _weakpoints.Length; i++) {
            _weakpoints[i].Deactivate();
        }

        _stage = 0;
        _blockingDone = false;
    }

    IEnumerator ChangeWeakpoint(int nextWeakpointIndex) {

        _animator.CrossFade("RIG_Boss_01|Boss_Block_Hurt_0" + nextWeakpointIndex, 0.1f);
        _weakpoints[nextWeakpointIndex - 1].Deactivate();

        yield return new WaitForSeconds(0.5f);
        _weakpoints[nextWeakpointIndex].Activate();
    }

}
