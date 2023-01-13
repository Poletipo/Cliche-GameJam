using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnim : MonoBehaviour
{

    public float Speed = 1;
    public float Height = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        pos.y = (Mathf.Sin(Time.time * Speed) * .5f + .5f) * Height;

        transform.position = pos;
    }
}
