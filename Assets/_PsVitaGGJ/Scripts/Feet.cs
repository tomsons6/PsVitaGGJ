using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feet : MonoBehaviour
{

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

    public Animator anim;

    [SerializeField]
    private bool gettingTickled = false;

    private bool finalStateReached = false;

    private MainGamePlayLogic mainScript;

    [SerializeField]
    HideSpot spot;
    public bool isHiding;

    // Use this for initialization
    void Start()
    {
        mainScript = FindObjectOfType<MainGamePlayLogic>();
    }

    // Update is called once per frame
    void Update()
    {

        if (finalStateReached) return;

        if (isHiding)
        {
            mainScript.debugText.text = "HIDE IN THE CORNER!";
        }

        if (peeLevel >= 1f)
        {
            OnPeeMax();
            return;
        }
        if (awakeLevel >= 1f)
        {
            OnAwakeMax();
            return;
        }
        if (gettingTickled)
        {
            peeLevel += (0.05f * 2f) * Time.deltaTime;
            awakeLevel += (0.07f * 2f) * Time.deltaTime;

        }
        else
        {
            peeLevel -= (0.005f * 2f) * Time.deltaTime;
            awakeLevel -= (0.035f * 2f) * Time.deltaTime;
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
        int rand = Random.Range(1, 4);
        if (rand == 3)
        {
            awakeLevel = .8f;
            StartCoroutine(HideEvent());
        }
        yield return new WaitForSeconds(1f);
        gettingTickled = false;
    }

    private void PlaySound()
    {
        float val = Random.value;

        if (val > 0.6f)
        {
            if (Random.value < peeLevel)
            {
                aSource.clip = sleepLaughClips[Random.Range(0, sleepLaughClips.Length)];
                aSource.Play();
            }
            else if (Random.value < awakeLevel)
            {
                aSource.clip = sleepGroanClips[Random.Range(0, sleepGroanClips.Length)];
                aSource.Play();
            }
        }
    }

    public IEnumerator HideEvent()
    {
        Debug.Log("start hiding");
        isHiding = true;
        yield return new WaitForSeconds(5f);
        if (spot.isHiding)
        {
            peeLevel += .3f;
            Debug.Log("is hidden");
        }
        else
        {
            Debug.Log("didnt make it");
            OnAwakeMax();
        }
        mainScript.debugText.text = "He did not see you";
        isHiding = false;
    }


    private void OnPeeMax()
    {
        print("peeee!");
        PS_pee.Play();
        aSource.clip = ohNoClip;
        aSource.Play();
        mainScript.AddSuccessPoint();
        finalStateReached = true;
    }

    private void OnAwakeMax()
    {
        print("im awake");
        aSource.clip = getAwayClip;
        aSource.Play();
        anim.SetBool("GetUp", true);
        mainScript.AddFailPoint();
        finalStateReached = true;
    }

    public void RemoveSliders()
    {
        peeSlider = null;
        awakeSlider = null;
    }
}
