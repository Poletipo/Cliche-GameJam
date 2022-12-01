using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, -transform.parent.rotation.y, 0); 
    }
}
