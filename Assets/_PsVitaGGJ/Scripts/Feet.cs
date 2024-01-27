using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feet : MonoBehaviour {

    public float peeLevel = 0f;
    public float awakeLevel = 0f;

    private Slider peeSlider;
    private Slider awakeSlider;

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


        if (gettingTickled) return;
        StartCoroutine(Tickle());
    }

    private IEnumerator Tickle()
    {
        gettingTickled = true;
        yield return new WaitForSeconds(1f);
        gettingTickled = false;
    }

    private void OnPeeMax()
    {
        print("peeee!");
        finalStateReached = true;
    }

    private void OnAwakeMax()
    {
        print("im awake");
        finalStateReached = true;
    }

    public void RemoveSliders()
    {
        peeSlider = null;
        awakeSlider = null;
    }
}
