using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSystem : MonoBehaviour {


    Vector3 fp;
    Vector3 lp;
    float dragDistance;

    public bool WasSwipedUp;
    public static TouchSystem Instance { get; private set; }
    public MainGamePlayLogic mainLogic;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        mainLogic = FindObjectOfType<MainGamePlayLogic>();

        dragDistance = Screen.height * 15 / 100;
    }

    // Update is called once per frame
    void Update () {
        Swipe();
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
                            //mainLogic.debugText.text = ("Right Swipe");
                        }
                        else
                        {   //Left swipe
                            //mainLogic.debugText.text = ("Left Swipe");
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            //debugField.text = ("Up Swipe");
                            StartCoroutine(SwipedUp());
                        }
                        else
                        {   //Down swipe
                            //mainLogic.debugText.text = ("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    //mainLogic.debugText.text = ("Tap");
                }
            }
        }
    }

    IEnumerator SwipedUp()
    {
        WasSwipedUp = true;
        yield return new WaitForSeconds(1f);
        WasSwipedUp = false;
    }
}
