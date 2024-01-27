using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{

    [SerializeField]
    UnityEngine.UI.Text debugField;

    Vector3 fp;
    Vector3 lp;
    float dragDistance;
    // Use this for initialization
    void Start()
    {
        dragDistance = Screen.height * 15 / 100;
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        //debugField.text = "Touch coordinate - " + Inputs.Instance.TouchScreen + "\n Something -  " + UnityEngine.PSVita.PSVitaInput.touchCountSecondary + "\n MouseInput x - " + Input.mousePosition.x +"\n MouseInput y - " + Input.mousePosition.y;
    }
    void Swipe()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            debugField.text = ("Right Swipe");
                        }
                        else
                        {   //Left swipe
                            debugField.text = ("Left Swipe");
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            debugField.text = ("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            debugField.text = ("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    debugField.text = ("Tap");
                }
            }
        }
    }
}
