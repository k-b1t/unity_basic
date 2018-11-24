using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triger_switch : MonoBehaviour {

    GameObject referenceObject;
    triger_switch referenceScript;
    Light light;
    public string nextLight = "gaisma1";
    public bool myLight = false;
   bool myCube = false;
    public bool OtherLight = false;
    bool activated = false;
    Renderer vizualObj;


    void Start()
    {
        referenceObject = GameObject.FindGameObjectWithTag(nextLight);
        referenceScript = referenceObject.GetComponent<triger_switch>();      
        light = GetComponent<Light>();
        light.enabled = false;

       vizualObj = GetComponentInChildren<MeshRenderer>();
       vizualObj.enabled = false;

        if (gameObject.tag == "gaisma1")
        {
            myLight = true;
            myCube = true;   
        }
    }
	void Update () {
        light.enabled = myLight;
        vizualObj.enabled = myCube;      
    }

    public void MethodToCall(bool state)
    {
        myLight = state;    
        myCube = true;
    } 

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && !activated && myLight)
        {
            referenceScript.MethodToCall(true);
            activated = true;
            myCube = false;
        }
    }
}
