using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01_AnimEvents : MonoBehaviour
{
    [SerializeField]
    IHitter[] hitters;
    [SerializeField]
    AttackState _attackState;

    public void ActivateHitter(int index)
    {
        hitters[index].Activate();
    }

    public void DeactivateHitter(int index)
    {
        hitters[index].Deactivate();
    }

    public void AttackAnimOver()
    {
        _attackState.AnimationOver();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
