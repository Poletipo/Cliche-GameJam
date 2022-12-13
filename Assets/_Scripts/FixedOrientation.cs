using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedOrientation : MonoBehaviour {

    void Update() {
        transform.rotation = Quaternion.Euler(0, -transform.parent.rotation.y, 0);
    }
}
