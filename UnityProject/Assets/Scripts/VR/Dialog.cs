using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {
    //This script handles the dialog interaction (the initial sound that plays when the player collides with the
    //triggered collider and then presses the corresponding button)

    private Hand rightHand;

	[Header ("Sound")]
	public AudioClip[] dialogClip;
    private AudioSource sound;
    private UIController UIController;

    [Header("Smoker UI options")]
    public DialogHelper dialogHelper;
    public BoxCollider hubrechtCollider;
    public GameObject motherBubble;
    public BoxCollider motherCollider;
    public GameObject cakeBubble;
    public GameObject anemoonBubble;
    public BoxCollider anemoonCollider;

    private void Start() {
        if (rightHand == null) {
			rightHand = GameObject.FindGameObjectWithTag("RightHand").GetComponent<Hand>();	//get right hand
        }
        UIController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
		sound = GetComponent<AudioSource>();
	}

    //TEST SOUND WITH "F" KEY
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (transform.parent.name == "Hubrecht Breukers")
            {
                sound.clip = dialogClip[0];
                sound.PlayOneShot(sound.clip);  //play dialog file
            }
        }
    }

    //PLAY CORRESPONDING DIALOG on clicking the seperate UI options
	public void playDialog(int number) {
        if (sound.clip != dialogClip[number])
        {
            sound.Stop();
            sound.clip = dialogClip[number];
            sound.PlayOneShot(sound.clip);  //play dialog file
        }
        else
        {
            if (!sound.isPlaying)
            {
                sound.Stop();
                sound.clip = dialogClip[number];
                sound.PlayOneShot(sound.clip);  //play dialog file
            }
        }

        if (transform.parent.name == "Hubrecht Breukers")
        {
            if (number == 1)
            {
                dialogHelper.Helper(1);
                dialogHelper.Helper(2);
            }
            else if (number == 2) {

                dialogHelper.Helper(3);
            }
            else if (number == 3)
            {
                dialogHelper.Helper(3);
            }
            else if (number == 4)
            {
                //Get the whiskey             
                UIController.AddObjective(transform.parent.name);
            }
        }


        if (transform.parent.name == "Mother2")
        {
            if (number == 1)
            {
                UIController.AddObjective("MotherSmoke");

                motherBubble.SetActive(false);
                anemoonBubble.SetActive(true);
                anemoonCollider.enabled = true;
            }
            if (number == 2)
            {
                motherBubble.SetActive(false);

                UIController.AddObjective("MotherCake");

                cakeBubble.SetActive(true);
            }
        }

        if (transform.parent.name == "Anemoon Hopster")
        {
            if (number == 1)
            {
                Debug.Log("asdfasdgfsags");
                //opens option yes & no

                dialogHelper.Helper(2);
            }
            if (number == 2)
            {
                Debug.Log("sdgvsdgf");
                dialogHelper.Helper(2);
            }
            if (number == 3)
            {
                Debug.Log("trhtrfstasdfasdgfsags");
                anemoonBubble.SetActive(false);
                motherBubble.SetActive(true);

                UIController.AddObjective("MotherTalk");

                //activate dialog collider on mother
                motherCollider.enabled = true;

                GameManager.cakeTalk = true;
            }
        }
    }

    private void audioHelper(int number)
    {
        if (sound.clip != dialogClip[number])
        {
            sound.Stop();
            sound.clip = dialogClip[number];
            sound.PlayOneShot(sound.clip);  //play dialog file
        }
        else
        {
            if (!sound.isPlaying)
            {
                sound.Stop();
                sound.clip = dialogClip[number];
                sound.PlayOneShot(sound.clip);  //play dialog file
            }
        }
    }
	
	//If player enters collider interact
	private void OnTriggerStay(Collider other) {
        


        //if colliding with Player collider		
        if (other.tag == "Head")
        {
            //Debug.Log("-------// Colliding with player //-------------");
            //if player presses the interaction button

            //OCULUS//
            if (SteamVR_Input._default.inActions.GrabPinch.GetState(rightHand.handType)) { 

            //VIVE//
            //if (SteamVR_Input._default.inActions.Teleport.GetState(rightHand.handType)) {
				Debug.Log("----------------------------Interact-------------------------");                

                if (GameManager.cakeTalk)
                {
                    GameManager.cakeTalk = false;

                    if (transform.parent.name == "Mother2")
                    {
                        audioHelper(2);

                        /*
                        if (sound.clip != dialogClip[2])
                        {
                            sound.clip = dialogClip[2];
                            sound.PlayOneShot(sound.clip);  //play dialog file
                        }
                        else
                        {
                            if (!sound.isPlaying)
                            {
                                sound.clip = dialogClip[2];
                                sound.PlayOneShot(sound.clip);  //play dialog file
                            }
                        }
                        */


                        UIController.AddObjective("MotherCake");
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                        motherBubble.SetActive(false);
                    }
                }
                else if (GameManager.radioTalk)
                {
                    Debug.Log("twsat this");


                    if (transform.parent.name == "Mother2")
                    {
                        audioHelper(3);

                        /*
                        if (sound.clip != dialogClip[3])
                        {
                            sound.clip = dialogClip[3];
                            sound.PlayOneShot(sound.clip);  //play dialog file
                        }
                        else
                        {
                            if (!sound.isPlaying)
                            {
                                sound.clip = dialogClip[3];
                                sound.PlayOneShot(sound.clip);  //play dialog file
                            }
                        }
                        */
                    }

                    UIController.AddObjective("Radio2");
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    motherBubble.SetActive(false);
                }

                else
                {
                    Debug.Log("talk to");

                    if (GameManager.level == 3)
                    {
                        dialogHelper.Helper(0);
                        dialogHelper.Helper(1);

                    }

                    /*
                    if (!sound.isPlaying) {
                        sound.clip = dialogClip[0];
                        sound.PlayOneShot(sound.clip);	//play dialog file
                    }
                    */

                    //if in/exhaling
                    audioHelper(0);

                    /*
                    if (sound.clip != dialogClip[0])
                    {
                        sound.clip = dialogClip[0];
                        sound.PlayOneShot(sound.clip);  //play dialog file
                    }
                    else
                    {
                        if (!sound.isPlaying)
                        {
                            sound.clip = dialogClip[0];
                            sound.PlayOneShot(sound.clip);  //play dialog file
                        }
                    }
                    */


                    gameObject.GetComponent<BoxCollider>().enabled = false; //disable collider so you can only do it once
                                                                            //gameObject.GetComponent<smokingLoop>().restartSmoking(smokingDelay);	//restart smoking after delay


                    //level1


                    //Activate objective on UI
                    if (transform.parent.name == "Mother")
                    {
                        //Makes the objective text appear and deactivates bubble
                        UIController.AddObjective("Mother");
                        motherBubble.SetActive(false);

                        

                        Debug.Log("Pass the cigarettes objective activated");
                    }
                    else if (transform.parent.name == "Hubrecht Breukers")
                    {
                        //wait a bit before enabling
                        dialogHelper.Helper(0);
                    }

                    //level2

                    /*else if (transform.parent.name == "Mother2")
                    {
                        //Makes the objective text appear and deactivates bubble
                        UIController.AddObjective("Mother2");
                        //motherBubble.SetActive(false);

                        Debug.Log("Smoke the cigarettes and get lung cancer");

                    }
                    */

                    if (transform.parent.name == "Anemoon Hopster")
                    {
                        dialogHelper.Helper(0);
                        dialogHelper.Helper(1);
                    }


                }
            }
        }
    }
}
