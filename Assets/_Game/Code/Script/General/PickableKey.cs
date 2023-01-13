using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableKey : PickableItem {

    public AudioClip KeySFX;

    public override void Collect(PlayerController playerController) {
        AudioManager.Instance.PlayAudio(KeySFX, transform.position);
        playerController.Keycount++;
        Destroy(gameObject);
    }
}
