using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpot : MonoBehaviour {

    public bool isHiding;

	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isHiding = true;
        }
        else
        {
            isHiding = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isHiding = false;
    }
}
