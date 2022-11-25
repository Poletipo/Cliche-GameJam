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
    private Color[] previousColor;

    private void Start()
    {
        previousColor = new Color[MeshRendererList.Length];
        for (int i = 0; i < MeshRendererList.Length; i++)
        {
            MeshRendererList[i].material.EnableKeyword("_EMISSION");
            previousColor[i] = MeshRendererList[i].material.GetColor("_EmissionColor");
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

        for (int i = 0; i < MeshRendererList.Length; i++)
        {
            MeshRendererList[i].material.SetColor("_EmissionColor", previousColor[i]);
        }

    }
}
