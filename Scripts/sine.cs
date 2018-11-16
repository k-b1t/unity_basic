using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sine : MonoBehaviour
{
  
	Vector3 startPos;
    public float amplitude = 0.5f;
    public float speed = 0.2f;
    protected void Start()


    {
        startPos = transform.position;
    }


    protected void Update()
    {
        float theta = Time.timeSinceLevelLoad / speed;
        float distance = amplitude * Mathf.Sin(theta);
        transform.position = startPos + Vector3.up * distance;
    }
}

