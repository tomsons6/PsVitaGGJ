using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feet : MonoBehaviour {

    public float peeLevel = 0f;
    public float awakeLevel = 0f;

    private Slider peeSlider;
    private Slider awakeSlider;

    public ParticleSystem PS_pee;

    public AudioSource aSource;
    public AudioClip[] sleepLaughClips;
    public AudioClip[] sleepGroanClips;

    public AudioClip ohNoClip;
    public AudioClip getAwayClip;

    [SerializeField]
    private bool gettingTickled = false;

    private bool finalStateReached = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (finalStateReached) return;

        if (gettingTickled)
        {
            if(peeLevel >= 1f)
            {
                OnPeeMax();
                return;
            }
            if(awakeLevel >= 1f)
            {
                OnAwakeMax();
                return;
            }

            peeLevel += 0.05f * Time.deltaTime;
            awakeLevel += 0.07f * Time.deltaTime;

            
        }
        else
        {
            if (peeLevel <= 0f)
            {
                return;
            }
            if (awakeLevel <= 0f)
            {
                return;
            }

            peeLevel -= 0.005f * Time.deltaTime;
            awakeLevel -= 0.035f * Time.deltaTime;
        }

        if (peeSlider == null || awakeSlider == null)
            return;
        peeSlider.value = peeLevel;
        awakeSlider.value = awakeLevel;
        
    }

    public void SetSliders(Slider _peeSlider, Slider _awakeSlider)
    {
        if (!peeSlider)
        {
            peeSlider = _peeSlider;
        }
        if (!awakeSlider)
        {
            awakeSlider = _awakeSlider;
        }
    }

    public void OnTickle()
    {

        if (finalStateReached) return;
        if (gettingTickled) return;
        StartCoroutine(Tickle());
    }

    private IEnumerator Tickle()
    {

        gettingTickled = true;
        PlaySound();
        yield return new WaitForSeconds(1f);
        gettingTickled = false;
    }

    private void PlaySound()
    {
        float val = Random.value;

        if (val > 0.6f)
        {
            if(Random.value < peeLevel)
            {
                aSource.clip = sleepLaughClips[Random.Range(0, sleepLaughClips.Length)];
                aSource.Play();
            }
            else if(Random.value < awakeLevel)
            {
                aSource.clip = sleepGroanClips[Random.Range(0, sleepGroanClips.Length)];
                aSource.Play();
            }
        }
    }

    

    private void OnPeeMax()
    {
        print("peeee!");
        PS_pee.Play();
        aSource.clip = ohNoClip;
        aSource.Play();
        finalStateReached = true;
    }

    private void OnAwakeMax()
    {
        print("im awake");
        aSource.clip = getAwayClip;
        aSource.Play();
        finalStateReached = true;
    }

    public void RemoveSliders()
    {
        peeSlider = null;
        awakeSlider = null;
    }
}
