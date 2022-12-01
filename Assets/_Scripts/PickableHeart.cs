using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableHeart : PickableItem
{

    public AudioClip heartSFX;

    public override void Collect(PlayerController playerController)
    {
        AudioManager.Instance.PlayAudio(heartSFX, transform.position);

        playerController.GetComponent<Health>().Heal(1);
        Destroy(gameObject);
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
