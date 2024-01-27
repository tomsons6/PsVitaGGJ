using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    [SerializeField]
    UnityEngine.UI.Text MainText;
    [SerializeField]
    float animationSpeed = 2f;
    public bool IsOpen;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowText()
    {
        MainText.text = "Press L1 and R1 to open";
    }
    public IEnumerator OpenDoor()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            Vector3 startPosition = transform.localRotation.eulerAngles;
            Vector3 endPosition = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 30f, transform.localEulerAngles.z);
            float t = 0f;
            while (t > 1f)
            {
                t = Time.deltaTime * animationSpeed;

                Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);

                transform.localPosition = currentPosition;
                yield return null;
            }

        }
    }
}
