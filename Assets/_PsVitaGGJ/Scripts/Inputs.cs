using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.PSVita;


public class Inputs : MonoBehaviour {

    public struct PSVitaControlls
    {
        public Vector2 thumbstick_left;
        public Vector2 thumbstick_right;

        public Vector2 touchScreen;

        public bool cross;
        public bool circle;
        public bool triangle;
        public bool square;

        public bool dpad_down;
        public bool dpad_right;
        public bool dpad_up;
        public bool dpad_left;

        public bool L1;
        public bool R1;


    }
    Vector3 fp;
    Vector3 lp;
    float dragDistance;
    public PSVitaControlls previousFrame;
    public PSVitaControlls currentFrame;

    public bool IsSquarePressed { get { return previousFrame.square == false && currentFrame.square == true; } }
    public bool IsCirclePressed { get { return previousFrame.circle == false && currentFrame.circle == true; } }
    public bool IsTrianglePressed { get { return previousFrame.triangle == false && currentFrame.triangle == true; } }
    public bool IsCrossPressed { get { return previousFrame.cross == false && currentFrame.cross == true; } }

    public bool WasSwipedUp = false;

    public bool IsDpadDownPressed { get { return previousFrame.dpad_down == false && currentFrame.dpad_down == true; } }
    public bool IsDpadRightPressed { get { return previousFrame.dpad_right == false && currentFrame.dpad_right == true; } }
    public bool IsDpadUpPressed { get { return previousFrame.dpad_up == false && currentFrame.dpad_up == true; } }
    public bool IsDpadLeftPressed { get { return previousFrame.dpad_left == false && currentFrame.dpad_left == true; } }

    public Vector2 GetThumbstickLeft { get { return currentFrame.thumbstick_left; } }
    public Vector2 GetThumbstickRight { get { return currentFrame.thumbstick_right; } }

    public Vector2 TouchScreen { get { return currentFrame.touchScreen; } }

    private string leftStickHorizontalAxis;
    private string leftStickVerticalAxis;

    private string rightStickHorizontalAxis;
    private string rightStickVerticalAxis;

    private KeyCode L1BtnKeyCode;
    private KeyCode R1BtnKeyCode;

    private KeyCode CrossBtnKeyCode;
    private KeyCode CircleBtnKeyCode;
    private KeyCode SquareBtnKeyCode;
    private KeyCode TriangleBtnKeyCode;

    private string DPadRightAxis;
    private string DPadLeftAxis;
    private string DPadUpAxis;
    private string DPadDownAxis;

    private static bool enableInput = true;
    private static float timeout = 0;

    public static void EnableInput(bool enable)
    {
        if (enable != enableInput)
        {
            enableInput = enable;

            if (enable == true)
            {
                timeout = 1.0f;
            }
        }
    }
    public static Inputs Instance { get; private set; }
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

    // Use this for initialization
    void Start () {
        AssignInputs();

    }
	
	// Update is called once per frame
	void Update () {
        if (timeout > 0.0f)
        {
            timeout -= Time.deltaTime;
        }

        if (enableInput == false || timeout > 0.0f)
        {
            previousFrame = new PSVitaControlls();
            currentFrame = new PSVitaControlls();
            return;
        }
        Thumbsticks();
        InputButtons();
        DpadButtons();
        ShoulderButtons();
        Swipe();
    }
    void AssignInputs()
    {
        leftStickHorizontalAxis = "Horizontal";
        leftStickVerticalAxis = "Vertical";

        rightStickHorizontalAxis = "rightstickhorizontal";
        rightStickVerticalAxis = "rightstickvertical";

        CrossBtnKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), "JoystickButton0", true);
        CircleBtnKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), "JoystickButton1", true);
        SquareBtnKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), "JoystickButton2", true);
        TriangleBtnKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), "JoystickButton3", true);

        L1BtnKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), "JoystickButton4", true);
        R1BtnKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), "JoystickButton5", true);

        DPadRightAxis = "dpad_horizontal";
        DPadLeftAxis = "dpad_horizontal";
        DPadUpAxis = "dpad_vertical";
        DPadDownAxis = "dpad_vertical";
    }

    void Thumbsticks()
    {
        currentFrame.thumbstick_left = new Vector2(Input.GetAxis(leftStickHorizontalAxis), Input.GetAxis(leftStickVerticalAxis));
        currentFrame.thumbstick_right = new Vector2(Input.GetAxis(rightStickHorizontalAxis), Input.GetAxis(rightStickVerticalAxis));
    }

    void InputButtons()
    {
        currentFrame.cross = Input.GetKey(CrossBtnKeyCode);
        currentFrame.circle = Input.GetKey(CircleBtnKeyCode);
        currentFrame.square = Input.GetKey(SquareBtnKeyCode);
        currentFrame.triangle = Input.GetKey(TriangleBtnKeyCode);
    }
    void DpadButtons()
    {
        currentFrame.dpad_right = Input.GetAxis(DPadRightAxis) > 0;
        currentFrame.dpad_left = Input.GetAxis(DPadLeftAxis) < 0;
        currentFrame.dpad_up = Input.GetAxis(DPadUpAxis) > 0;
        currentFrame.dpad_down = Input.GetAxis(DPadDownAxis) < 0;
    }
    void ShoulderButtons()
    {
        currentFrame.L1 = Input.GetKey(L1BtnKeyCode);
        currentFrame.R1 = Input.GetKey(R1BtnKeyCode);
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
                            //debugField.text = ("Right Swipe");
                        }
                        else
                        {   //Left swipe
                            //debugField.text = ("Left Swipe");
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            //debugField.text = ("Up Swipe");
                            WasSwipedUp = true;
                        }
                        else
                        {   //Down swipe
                            //debugField.text = ("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    //debugField.text = ("Tap");
                }
            }
        }
        WasSwipedUp = false;
    }
}
