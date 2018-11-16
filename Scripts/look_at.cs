using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look_at : MonoBehaviour {


    public Transform target;

    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }
}
