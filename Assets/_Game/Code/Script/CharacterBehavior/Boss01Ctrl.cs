using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Ctrl : MonoBehaviour {

    public BossState CurrentState;

    private BossState _nextState;

    void Start() {
        CurrentState.EnterState(null);
    }

    void Update() {

        _nextState = CurrentState.UpdateState();

        if (_nextState != null) {
            CurrentState.LeaveState();
            BossState previousState = CurrentState;
            CurrentState = _nextState;
            CurrentState.EnterState(previousState);
        }
    }
}
