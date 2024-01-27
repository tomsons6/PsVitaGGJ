using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSounds : MonoBehaviour {

    public AudioClip footstep_Combined;
    public AudioClip footstep_1;
    public AudioClip footstep_2;

    public AudioSource aSource;

    private bool stepOne = false;

    // Use this for initialization
    void Start () {
        aSource.clip = footstep_Combined;
	}
	
	// Update is called once per frame
	void Update () {
        //Movement(new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1")));
        float hor = Input.GetAxis("Horizontal1");
        float ver = Input.GetAxis("Vertical1");

        if(hor > 0.1f || hor < -0.1f || ver > 0.1f || ver < -0.1f)
        {
            PlayFootstep();
        }
        else
        {
            StopFootstep();
        }
    }

    public void PlayFootstep()
    {
        if (aSource.isPlaying) return;
        aSource.Play();

        if (stepOne)
        {
            //aSource.PlayOneShot()
        }
        else
        {

        }
    }
    public void StopFootstep()
    {
        if (!aSource.isPlaying) return;
        aSource.Pause();

        if (stepOne)
        {
            //aSource.PlayOneShot()
        }
        else
        {

        }
    }
}
