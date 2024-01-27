using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGamePlayLogic : MonoBehaviour {

    bool isLookingAtFeet = false;

    [SerializeField]
    public UnityEngine.UI.Text debugText;

    DoorScript[] doorsArray;
    Feet[] feetsArray;

	// Use this for initialization
	void Start () {
        doorsArray = FindObjectsOfType<DoorScript>();
        feetsArray = FindObjectsOfType<Feet>();
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
                debugText.text = TouchSystem.Instance.WasSwipedUp.ToString();
                if (TouchSystem.Instance.WasSwipedUp || Input.GetKey(KeyCode.P))
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
                foreach (DoorScript door in doorsArray)
                {
                    door.ShowText();
                }
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
