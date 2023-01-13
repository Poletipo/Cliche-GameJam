using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour {

    [SerializeField] private IHitter _hitter;
    [SerializeField] private AudioClip[] _bootSFX;

    public void ActivateHitter() {
        _hitter.Activate();
    }

    public void DeactivateHitter() {
        _hitter.Deactivate();
    }

    public void PlayBootSound(int foot) {
        AudioManager.Instance.PlayAudio(_bootSFX[foot], transform.position, .25f);
    }

}
