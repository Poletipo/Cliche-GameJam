using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableKey : PickableItem
{
    public override void Collect(PlayerController playerController)
    {
        playerController.keycount++;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
