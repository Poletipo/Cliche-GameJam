using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField]
    private IHitter hitter;

    [SerializeField]
    private AudioClip[] bootSFX;

    public void ActivateHitter()
    {
        hitter.Activate();
    }

    public void DeactivateHitter()
    {
        hitter.Deactivate();
    }


    public void PlayBootSound(int foot)
    {
        AudioManager.Instance.PlayAudio(bootSFX[foot], transform.position,.25f);
    }

}
