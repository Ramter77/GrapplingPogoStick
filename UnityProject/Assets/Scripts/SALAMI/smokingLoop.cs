using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokingLoop : MonoBehaviour {
    //This script handles emitting smoke in a loop and plays the in/exhale sounds

    private ParticleSystem pS;

	[Header ("Cooldown")]
    public float startDelay;
	public int minCD = 10;
	public int maxCD = 15;

    [Header ("Sounds")]
    public AudioClip inhaleClip;
    public AudioClip exhaleClip;
    private AudioSource sound;
    public float inhaleDelay;

	void Start () {
		pS = GetComponentInChildren<ParticleSystem>();

        sound = GetComponent<AudioSource>();

        restartSmoking(startDelay);               
	}

    public void restartSmoking(float delay) {
        float rnd = Random.Range(minCD, maxCD);
        InvokeRepeating("Smoke", delay, rnd);		//call Smoke() after <startCD> seconds every <minCD-maxCD> seconds
    }

    public void StopSmoking() {
        CancelInvoke();
    }

	void Smoke() {
		pS.Play();

        sound.clip = exhaleClip;
		sound.PlayOneShot(sound.clip);	//play exhale sound

        //wait some seconds and then play inhale sound
        StartCoroutine(inhaleSound());
	}

    IEnumerator inhaleSound() {
        yield return new WaitForSeconds(inhaleDelay);
        
        sound.clip = inhaleClip;
		sound.PlayOneShot(sound.clip);	//play inhale sound
    }
}
