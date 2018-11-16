using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_0 : MonoBehaviour
{

    public float move_speed = 1;
    public float move_side_speed = 1;



    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += Vector3.forward * Time.deltaTime * move_speed;
        }  
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position += Vector3.back * Time.deltaTime * move_speed;       
        }
        if (Input.GetKey(KeyCode.D))
        {
          gameObject.transform.position += Vector3.right * Time.deltaTime * move_speed;          
        }
        if (Input.GetKey(KeyCode.A))
        {
           gameObject.transform.position += Vector3.left * Time.deltaTime * move_speed;
        }
    }
}
