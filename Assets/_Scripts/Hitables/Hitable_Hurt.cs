using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable_Hurt : IHitable
{
    public Health Hp;
    public MeshRenderer MeshRenderer;

    public float flashTimer = 0.1f;
    private float flashStart;
    private bool isFlashing = false;
    public Color FlashColor;

    public override void Hit(HitterValue value)
    {
        bool isHurt = Hp.Hurt(value.dmg);
        if (isHurt)
        {
            StartFlash();
        }
    }

    private void StartFlash()
    {
        isFlashing = true;
        MeshRenderer.material.EnableKeyword("_EMISSION");
        MeshRenderer.material.SetColor("_EmissionColor", FlashColor);
        flashStart = Time.time;
    }

    private void Update()
    {
        if(flashStart + flashTimer < Time.time && isFlashing)
        {
            StopFlash();
        }
    }

    private void StopFlash()
    {
        isFlashing = false;
        MeshRenderer.material.SetColor("_EmissionColor", Color.black);
    }
}
