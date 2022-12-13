using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableHeart : PickableItem {

    public AudioClip HeartSFX;

    public override void Collect(PlayerController playerController) {
        AudioManager.Instance.PlayAudio(HeartSFX, transform.position);

        playerController.GetComponent<Health>().Heal(1);
        Destroy(gameObject);
    }
}
