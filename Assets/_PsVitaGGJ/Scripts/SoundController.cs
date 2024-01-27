using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public AudioClip[] monsterClips;
    public AudioClip doorOpenClip;
    public AudioClip tickleClip;

    public AudioSource monsterSource;
    public AudioSource generalSource;

    // Use this for initialization
    void Start () {
        StartCoroutine(MonsterSounds());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator MonsterSounds()
    {
        float time = Random.Range(3f, 6f);
        yield return new WaitForSeconds(time);
        monsterSource.clip = monsterClips[Random.Range(0, monsterClips.Length)];
        SetRandomPitch(monsterSource, 0.95f, 1.1f);
        monsterSource.Play();
        StartCoroutine(MonsterSounds());
    }

    public void DoorSound()
    {
        SetRandomPitch(generalSource);
        generalSource.PlayOneShot(doorOpenClip, 1.2f);
    }

    public void PlayTickleSound()
    {
        SetRandomPitch(generalSource);
        generalSource.PlayOneShot(tickleClip, 0.6f);
    }

    private void SetRandomPitch(AudioSource source, float min = 0.95f, float max = 1.1f)
    {
        source.pitch = Random.Range(min, max);
    }
}
