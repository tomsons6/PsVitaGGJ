using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGamePlayLogic : MonoBehaviour {

    bool isLookingAtFeet = false;

    [SerializeField]
    UnityEngine.UI.Text debugText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CastRayCast();
	}
    void CastRayCast()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f))
        {
            if (hitInfo.transform.CompareTag("Feet"))
            {
                isLookingAtFeet = true;
                Debug.Log("Look Feet");
                if (Inputs.Instance.WasSwipedUp)
                {
                    StartCoroutine(ShowText());
                }
            }
            else
            {
                isLookingAtFeet = false;
            }
            if (hitInfo.transform.CompareTag("Door"))
            {
                Debug.Log("Open door");
            }
        }

    }
    IEnumerator ShowText()
    {
        debugText.text = "Tickeling feet";
        yield return new WaitForSeconds(1f);
        debugText.text = "";
    }
}
