using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triger_0 : MonoBehaviour {


	void Start () {
		
	}
	

	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
