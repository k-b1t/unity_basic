using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_1 : MonoBehaviour
{


    public float move_speed = 1;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += gameObject.transform.forward * Time.deltaTime * move_speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += gameObject.transform.forward * Time.deltaTime * move_speed * -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 cameraLeftVector = Camera.main.transform.localEulerAngles;
            gameObject.transform.Translate(cameraLeftVector * (0.001f * move_speed));
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 cameraLeftVector = Camera.main.transform.localEulerAngles;
            gameObject.transform.Translate(cameraLeftVector * -(0.001f * move_speed));
        }
    }
}
