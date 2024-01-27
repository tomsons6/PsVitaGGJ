using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    [SerializeField]
    UnityEngine.UI.Text MainText;
    [SerializeField]
    float animationSpeed = 2f;
    public bool IsOpen = false;
    public bool TextShowing = false;

    public void ShowText()
    {
        TextShowing = true;
        MainText.text = "Press L1 and R1 to open";
    }
    public void ClearText()
    {
        TextShowing = false;
        MainText.text = "";
    }
    public IEnumerator OpenDoor()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            Vector3 startRotation = transform.localRotation.eulerAngles;
            Vector3 endRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 80f, transform.localEulerAngles.z);
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * animationSpeed;

                Vector3 currentPosition = Vector3.Lerp(startRotation, endRotation, t);

                transform.localEulerAngles = currentPosition;
                yield return null;
            }

        }
    }
}
