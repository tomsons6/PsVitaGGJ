using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGamePlayLogic : MonoBehaviour {

    bool isLookingAtFeet = false;

    [SerializeField]
    public UnityEngine.UI.Text debugText;
    public Slider peeSlider;
    public Slider awakeSlider;

    DoorScript[] doorsArray;
    [SerializeField]Feet[] feetsArray;

    [SerializeField]private Feet currentFeet;

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
                if (currentFeet == null)
                {
                    currentFeet = GetFeetFromArray(hitInfo.transform.gameObject);
                    currentFeet.SetSliders(peeSlider, awakeSlider);
                    peeSlider.value = currentFeet.peeLevel;
                    awakeSlider.value = currentFeet.awakeLevel;
                }
                //Debug.Log("Look Feet");
                debugText.text = TouchSystem.Instance.WasSwipedUp.ToString();
                if (TouchSystem.Instance.WasSwipedUp || Input.GetKey(KeyCode.P))
                {
                    
                    //StartCoroutine(ShowText());
                    TickleFeet();
                }
            }
            else
            {
                isLookingAtFeet = false;

                if(currentFeet) currentFeet.RemoveSliders();
                currentFeet = null;
                peeSlider.value = 0f;
                awakeSlider.value = 0f;
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

    private void TickleFeet()
    {
        print("feet name: " + currentFeet.gameObject.name);
        currentFeet.OnTickle();
    }

    private Feet GetFeetFromArray(GameObject obj)
    {
        foreach(Feet f in feetsArray)
        {
            if(f.gameObject == obj)
            {
                return f;
            }
        }
        return null;
    }
}
