using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : MonoBehaviour
{

    public abstract void EnterState(BossState previousState);
    public abstract void LeaveState();

    public abstract BossState UpdateState();
}
