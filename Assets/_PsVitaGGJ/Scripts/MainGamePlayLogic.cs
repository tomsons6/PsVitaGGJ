﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGamePlayLogic : MonoBehaviour
{
    public int successPoints = 0;
    public int failPoints = 0;

    public int maxSuccessPoints = 3;
    public int maxFailPoints = 2;

    public bool gameRunning = false;

    //bool isLookingAtFeet = false;

    //[SerializeField]
    //public UnityEngine.UI.Text debugText;
    public Text PointsText;
    public Slider peeSlider;
    public Slider awakeSlider;
    public Animator tickleAnimator;

    //[SerializeField]Feet[] feetsArray;

    //[SerializeField]private Feet currentFeet;

    [SerializeField]private SoundController soundsContr;
    [SerializeField] public UIController ui;

    DoorScript tempDoor;
    Feet tempFeet;
    // Use this for initialization
    void Start () {
        //feetsArray = FindObjectsOfType<Feet>();
        PointsText.text = "Woken: " + successPoints + "/" + maxSuccessPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameRunning) return;
        CastRayCast();
    }

    void CastRayCast()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 20f))
        {
            //if (hitInfo.transform.GetComponent<Feet>() != null)
            //{
            //    tempFeet = hitInfo.transform.GetComponent<Feet>();
            //    tempFeet.SetSliders(peeSlider, awakeSlider);
            //    peeSlider.value = tempFeet.peeLevel;
            //    awakeSlider.value = tempFeet.awakeLevel;
            //}
            //else
            //{
            //    tempFeet = null;
            //}
            if (hitInfo.transform.CompareTag("Feet"))
            {
                if(tempFeet == null)
                {
                    tempFeet = hitInfo.transform.GetComponent<Feet>();
                    tempFeet.SetSliders(peeSlider, awakeSlider);
                    peeSlider.value = tempFeet.peeLevel;
                    awakeSlider.value = tempFeet.awakeLevel;
                }

                if (!tempFeet.isHiding && !tempFeet.finalStateReached)
                {
                    ui.SetText("Swipe up");
                }
                else if(tempFeet.finalStateReached)
                {
                    ui.SetText("");
                }
                //isLookingAtFeet = true;
                //if (tempFeet == null)
                //{
                //    //tempFeet = GetFeetFromArray(hitInfo.transform.gameObject);
                //    currentFeet.SetSliders(peeSlider, awakeSlider);
                //    peeSlider.value = currentFeet.peeLevel;
                //    awakeSlider.value = currentFeet.awakeLevel;
                //}
                //Debug.Log("Look Feet");
                if (TouchSystem.Instance.WasSwipedUp || Input.GetKey(KeyCode.P))
                {
                    //StartCoroutine(ShowText());
                    if (tempFeet.gettingTickled) return;
                    TickleFeet();
                }
            }
            else
            {
                //isLookingAtFeet = false;

                if(tempFeet) tempFeet.RemoveSliders();
                
                peeSlider.value = 0f;
                awakeSlider.value = 0f;
                if (tempFeet != null)
                {
                    if (!tempFeet.isHiding || tempFeet.finalStateReached)
                    {
                        ui.SetText("");
                    }
                }
                //ui.SetText("");
                tempFeet = null;
            }
            if (hitInfo.transform.CompareTag("Door"))
            {
                if (tempDoor == null)
                {
                    tempDoor = hitInfo.transform.GetComponent<DoorScript>();
                }
                else
                {
                    if (!tempDoor.IsOpen)
                    {
                        tempDoor.ShowText();
                    }
                    if (Inputs.Instance.IsL1Pressed && Inputs.Instance.IsR1Pressed || Input.GetKeyDown(KeyCode.O))
                    {
                        OpenDoor();
                    }
                    Debug.Log("Open door");
                }
            }
            else if(tempDoor != null)
            {
                //if(tempFeet != null)
                //{
                //    if (tempFeet.isHiding)
                //    {
                //        tempDoor.ClearText();
                //    }
                //}
                tempDoor.ClearText();
                tempDoor = null;
            }
        }

    }

    void OpenDoor()
    {
        if (!tempDoor.IsOpen)
        {
            soundsContr.DoorSound();
            StartCoroutine(tempDoor.OpenDoor());
        }

    }

    private void TickleFeet()
    {
        //print("feet name: " + currentFeet.gameObject.name);
        //tickleAnimator.SetBool("isTickling", true);
        tickleAnimator.SetTrigger("tickle");
        soundsContr.PlayTickleSound();
        tempFeet.OnTickle();
        //tickleAnimator.ResetTrigger("tickle");
        //tickleAnimator.SetBool("isTickling", false);
    }

    //private Feet GetFeetFromArray(GameObject obj)
    //{
    //    foreach(Feet f in feetsArray)
    //    {
    //        if(f.gameObject == obj)
    //        {
    //            return f;
    //        }
    //    }
    //    return null;
    //}

    public void AddSuccessPoint()
    {
        successPoints += 1;
        PointsText.text = "Woken: " + successPoints + "/" + maxSuccessPoints;
        if (successPoints >= maxSuccessPoints)
        {
            GameEnd(true);
        }
    }

    public void AddFailPoint()
    {
        failPoints += 1;
        if(failPoints >= maxFailPoints)
        {
            GameEnd(false);
        }
    }



    public void GameStart()
    {
        gameRunning = true;
        Cursor.lockState = CursorLockMode.Locked;
        ui.SwitchTo_Game();
    }

    private void GameEnd(bool gameSuccess)
    {
        gameRunning = false;
        ui.SwitchTo_MainMenu();
        Cursor.lockState = CursorLockMode.None;
        ui.SwitchTo_WinLose(gameSuccess);
    }
}
