using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move_0 : MonoBehaviour
{

    public float speed = 6.0f;

    void Update()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
    }
}
