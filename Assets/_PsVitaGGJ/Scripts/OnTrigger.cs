using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OnTrigger : MonoBehaviour {

    public string checkTag = "";

    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerExitEvent;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == checkTag)
        {
            if(OnTriggerEnterEvent != null)
                OnTriggerEnterEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == checkTag)
        {
            if (OnTriggerExitEvent != null)
                OnTriggerExitEvent.Invoke();
        }
    }
}
