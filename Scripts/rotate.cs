using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public bool rot_x = false;
    public float speed_x = 1f;
    public bool rot_y = false;
    public float speed_y = 1f;
    public bool rot_z = false;
    public float speed_z = 1f;



    void Update()
    {

     


        if (rot_x)
        {
        transform.Rotate(Vector3.right * Time.deltaTime * speed_x, Space.World);
        }

        if (rot_y)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed_y, Space.World);
        }

        if (rot_z)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed_z, Space.World);
        }



    }
}
