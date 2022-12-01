using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableKey : PickableItem
{
    public AudioClip keySFX;
    public override void Collect(PlayerController playerController)
    {
        AudioManager.Instance.PlayAudio(keySFX, transform.position);
        playerController.Keycount++;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
