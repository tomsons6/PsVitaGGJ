using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour {
    private RaycastHit hitInfo;


    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
        CastRayCast();
	}
    void CastRayCast()
    {
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hitInfo, 20f))
        {
            if (hitInfo.transform.CompareTag("Feet"))
            {
                Debug.Log("Look Feet");
            }
        }
    }
}
