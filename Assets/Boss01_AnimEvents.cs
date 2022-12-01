using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01_AnimEvents : MonoBehaviour
{
    [SerializeField]
    IHitter[] hitters;
    [SerializeField]
    AttackState _attackState;
    [SerializeField]
    AudioClip boomSFX;

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
    
    public void PlayBoomSound(int index)
    {
        AudioManager.Instance.PlayAudio(boomSFX, hitters[index].transform.position);
    }


}
