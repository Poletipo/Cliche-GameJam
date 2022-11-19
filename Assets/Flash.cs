using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{

    public Renderer[] MeshRendererList;

    public float flashTimer = 0.1f;
    private float flashStart;
    private bool isFlashing = false;
    public Color FlashColor;

    private void Start()
    {

        foreach (Renderer item in MeshRendererList)
        {
            item.material.EnableKeyword("_EMISSION");
        }
    }




    public void StartFlash()
    {
        isFlashing = true;
        foreach (Renderer item in MeshRendererList)
        {
            item.material.SetColor("_EmissionColor", FlashColor);
        }
        flashStart = Time.time;
    }

    private void Update()
    {
        if (isFlashing &&flashStart + flashTimer < Time.time)
        {
            StopFlash();
        }
    }

    public void StopFlash()
    {
        isFlashing = false;
        foreach (Renderer item in MeshRendererList)
        {
            item.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
