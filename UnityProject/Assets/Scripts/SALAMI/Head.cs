 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Head : MonoBehaviour {
    //This script handles smoking the cigarette through the microphone and plays the corresponding sounds

    public bool debug;
    
    [Header ("Parameters")]
	[Range(-5, 5)]
	public float mThreshold = 2.5f;
    public bool isInput;
	public ParticleSystem mParticleSystem;    

    [Header ("Microphone(Cigarette) sounds")]
    [Tooltip ("Microphone uses the attached audio source")]
    public AudioSource audioSource;
    public AudioClip mAudioStream = null;
	private Coroutine mCurrentSmoke = null;

    [Header ("Player sounds")]
    public AudioClip inhaleClip;
    public AudioClip exhaleClip;
    public AudioSource playerSound;
    public float inhaleDelay;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.loop = true;

        if (debug) {
            foreach (var device in Microphone.devices)
            {
                Debug.Log(device);
                Debug.Log(Microphone.IsRecording(device));
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
		if (debug) { Debug.Log("Entered head collider: " + other); }

		var cigarette = other.gameObject.GetComponent<Cigarette>();
		if (cigarette && GameManager.smokingAllowed) {
			mCurrentSmoke = StartCoroutine(cigarette.Drag(this));

            smokeSound(true);

			mParticleSystem.Stop();
		}
	}

    private void OnTriggerStay(Collider other)
    {
        var cigarette = other.gameObject.GetComponent<Cigarette>();
        if (cigarette && GameManager.smokingAllowed)
        {
            TestForAudioInput();
        }        
    }

    private void OnTriggerExit(Collider other) {
		if (debug) { Debug.Log("Exited head collider: " + other); }

		if (other.gameObject.tag == "Cigarette" && GameManager.smokingAllowed) {
			if (mCurrentSmoke != null) {
				StopCoroutine(mCurrentSmoke);

                if (isInput)
                {
                    mParticleSystem.Play();

                    smokeSound(false);
                }
			}
		}
	}

    private void smokeSound(bool inhale) {
        /*
        if (playerSound.isPlaying) {
            playerSound.Stop();
        }
        */

        if (!playerSound.isPlaying)
        {
            if (inhale)
            {
                playerSound.clip = inhaleClip;
            }
            else
            {
                playerSound.clip = exhaleClip;
            }
            //play corresponding sound
            playerSound.PlayOneShot(playerSound.clip);	//play inhale sound

            Debug.Log("Play smoke Sound");
        }
    }

    public float Sum(params float[] array)
    {
        float result = 0;
        for (int i = 0; i < array.Length; i++)
        {
            result += array[i];
        }
        return result;
    }

    public float Average(params float[] customerssalary)
    {
        float sum = Sum(customerssalary);
        float result = (float)sum / customerssalary.Length;
        return result;
    }

    public bool TestForAudioInput() {
		//Set the max amount of samples for mic: 44100
		int length = audioSource.clip.samples * audioSource.clip.channels;
		float[] samples = new float[length];

        //Get data
        audioSource.clip.GetData(samples, 0);
        float averageSample = Average(samples) * 100000;

        //If within threshold
        isInput = averageSample > mThreshold ? true: false;

        if (debug) {
            Debug.Log(">>>>>>>>>>>>>Samples: " + averageSample + " therfore isInput: " + isInput);
        }

		return isInput;
	}
}
