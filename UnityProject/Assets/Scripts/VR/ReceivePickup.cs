using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivePickup : MonoBehaviour {
	//This script handles receiving pickups, playing a thanks sound and changes the UI accordingly

	private GameManager gm;
    private toggleModel toggleModelScript;

	[Header ("Sound")]
	public AudioClip[] thanksClip;
    private AudioSource sound;

    [Header("Booleans")]
	public bool alreadyReceivedCigarette;
	private bool alreadyReceivedBottle;
    public bool alreadyReceivedSlice;

    void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		}		
		sound = GetComponent<AudioSource>();

        //toggleModelScript = transform.parent.parent.Find("NCCigarette").GetComponent<toggleModel>();

        //Debug.Log(transform.parent.parent.name);
        toggleModelScript = transform.parent.parent.GetChild(0).GetChild(0).GetChild(0).GetComponent<toggleModel>();
    }
	
	private void OnTriggerEnter(Collider other) {
		//if its a cigarette
		string tag = other.tag;
		if (tag == "Cigarette") {
			Debug.Log("Distributed: " + other.name);

            if (GameManager.cigaretteDistributionEnabled)
            {
                //if not already received, which changes to true on successful action
                if (!alreadyReceivedCigarette)
                {
                    alreadyReceivedCigarette = true;

                    toggleModelScript.enableModel();

                    //increment amount of cigarettes distributed & check if objective completed
                    gm.incrementCigarette();    //pass   gameObject.name   ?

                    sound.clip = thanksClip[0];     //thanks 0
                    sound.PlayOneShot(sound.clip);  //play thanks file

                    Debug.Log("seadfsef" + other.transform.parent.gameObject);
                    other.gameObject.SetActive(false);
                }
            }
		}

		else if (tag == "Bottle") {            
            Debug.Log("Received Bottle");

            if (GameManager.bottleDistributionEnabled)
            {
                //check if recipient is allowed to get that item
                if (transform.parent.parent.name == "Hubrecht Breukers")
                {
                    gm.incrementBottle();

                    sound.clip = thanksClip[1];     //thanks 0
                    sound.PlayOneShot(sound.clip);  //play thanks file

                    other.gameObject.SetActive(false);
                }
            }
		}

        else if (tag == "cakeSlice")
        {
            Debug.Log("Received cake slice: cakeDist? " + GameManager.cakeDistributionEnabled);

            if (GameManager.cakeDistributionEnabled)
            {
                if (!alreadyReceivedSlice)
                {
                    alreadyReceivedSlice = true;

                    Debug.Log("cake slice");

                    gm.incrementCakeSlices();

                    sound.clip = thanksClip[1];     //thanks 0
                    sound.PlayOneShot(sound.clip);  //play thanks file

                    other.gameObject.SetActive(false);
                }
            }
        }
    }
}
