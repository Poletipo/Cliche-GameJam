using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField]
    private IHitter hitter;

    public void ActivateHitter()
    {
        hitter.Activate();
    }

    public void DeactivateHitter()
    {
        hitter.Deactivate();
    }

}
