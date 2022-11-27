using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Ctrl : MonoBehaviour
{

    public BossState CurrentState;
    BossState _nextState;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState.EnterState(null);
    }

    // Update is called once per frame
    void Update()
    {

        _nextState = CurrentState.UpdateState();

        if(_nextState != null)
        {
            CurrentState.LeaveState();
            BossState previousState = CurrentState;
            CurrentState = _nextState;
            CurrentState.EnterState(previousState);
        }


    }
}
