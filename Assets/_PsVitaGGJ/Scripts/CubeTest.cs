using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour {

    [SerializeField]
    UnityEngine.UI.Text debugField;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        debugField.text = "Touch coordinate - " + Inputs.Instance.TouchScreen + "\n Something -  " + UnityEngine.PSVita.PSVitaInput.touchCountSecondary + "\n Right thumb input - " + Inputs.Instance.GetThumbstickRight.ToString();
	}
}
