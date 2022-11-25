using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableKey : PickableItem
{
    public override void Collect(PlayerController playerController)
    {
        playerController.Keycount++;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
