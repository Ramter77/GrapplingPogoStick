using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Movement : MonoBehaviour
{
    //This script handles movement and footstep sounds
    private Hand leftHand;

    public float moveSpeed = 1;
    private Transform headCamera;

    [Header("Sound")]
    [Tooltip("Step sound cooldown")]
    public float stepCD;
    public AudioSource playerSound;
    public AudioClip stepsSound;
    private bool firstStep;
    private float stepTimer;

    public SteamVR_ActionSet actionSet;

    void Awake()
    {
        leftHand = GetComponent<Hand>();
        headCamera = transform.parent.Find("VRCamera");
        Debug.Log("Found camera: " + headCamera);

        actionSet.ActivatePrimary();
    }

    void Move()
    {
        Vector3 forwardM = new Vector3(headCamera.forward.x, 0f, headCamera.forward.z);
        transform.root.position += forwardM * moveSpeed * Time.deltaTime;
    }

    void MoveS()
    {
        Quaternion orientation = headCamera.transform.rotation;
        //Vector3 forwardM = new Vector3(headCamera.forward.x, 0f, headCamera.forward.z);

        var touchPadVector = SteamVR_Input.platformer.inActions.Move.GetAxis(leftHand.handType);

        Vector3 moveDirection = orientation * Vector3.forward * touchPadVector.y + orientation * Vector3.right * touchPadVector.x;
        Vector3 pos = transform.root.position;
        pos.x += moveDirection.x * moveSpeed * Time.deltaTime;
        pos.z += moveDirection.z * moveSpeed * Time.deltaTime;
        transform.root.position = pos;
    }

    void MoveSound()
    {
        //First Step SOUND
        if (firstStep)
        {
            playerSound.clip = stepsSound;
            playerSound.PlayOneShot(playerSound.clip);
            firstStep = false;
        }

        //Step SOUND
        if (stepTimer <= 0)
        {
            playerSound.clip = stepsSound;
            playerSound.loop = true;
            playerSound.PlayOneShot(playerSound.clip);
            stepTimer = stepCD;
        }
        else
        {
            stepTimer -= Time.deltaTime;
        }
    }

    void Update()
    {
        //MOVE

        //OCULUS//
        bool move = SteamVR_Input._default.inActions.GrabPinch.GetState(leftHand.handType);

        //VIVE//
        //bool move = SteamVR_Input._default.inActions.Teleport.GetState(leftHand.handType);


        Vector2 move2 = (SteamVR_Input.platformer.inActions.Move.GetAxis(leftHand.handType));
        //Debug.Log(move);




        if (move2 != null)
        {
            MoveS();

        }

        if (move2.x != 0 && move2.y != 0)
        {
            //Todo: only play sound when grounded
            //MoveSound();
        }

        /*  if (move) {            
                    Move();
                    MoveSound();            
        }   */

        else
        {  //stop sound
            playerSound.loop = false;
            if (playerSound.clip == stepsSound && playerSound.isPlaying)
            {
                playerSound.Stop();
            }
        }
    }
}