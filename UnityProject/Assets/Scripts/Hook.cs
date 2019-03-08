using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Hook : MonoBehaviour {
	#region Variables
	#region VR variables
	[Header ("VR")]
	public bool enable_VR;
	public Hand hand;
	private bool trigger;
	#endregion

	#region Hook variables
	[Header ("Hook")]
	public GameObject hookHolder;
	public GameObject hook;
	private LineRenderer rope;
	public float maxHookTravelSpeed;
	public float maxHookTravelDistance;

	//Private control
	public static bool fired;
	public bool hooked;
	public GameObject hookedObject;
	#endregion

	#region Player variables
	[Header ("Player")]
	public float maxPlayerTravelSpeed;
	[Tooltip ("Player is stopped when he is this amount of units away from his destination (hooked object)")]
    public float minPlayerDistanceToDestination = 1f;
	private bool grounded, reachedDestination;
	private float currentPlayerDistanceToHook, playerDistanceToHook;
	private Rigidbody playerRB;
	#endregion
	#endregion

    #region Start
    private void Start() {
		if (!hand) {
			hand = GetComponentInChildren<Hand>();
		}

		playerRB = GetComponent<Rigidbody>();

		if (!hook) {
			hook = GameObject.FindObjectOfType<HookDetector>().gameObject;
		}

		if (!rope) {
			rope = hook.GetComponent<LineRenderer>();
		}
	}
	#endregion

    void Update()
	{
		#region Input
		//Input
		if (!enable_VR) {
			if (Input.GetKey(KeyCode.Mouse0)) {
				if (!fired) {
					fired = true;
				}
			}
			else {
				fired = false;
				ReturnHook();
			}
		}

		else {
			trigger = SteamVR_Input._default.inActions.GrabPinch.GetState(hand.handType);

			//Debug.Log("LeftHand: "+trigger1 + "RightHand: "+trigger2);
			//VR input
			if (!fired) {
				if (trigger) {
					fired = true;
				}
			}
		}
		#endregion

		#region Throw grapple
		if (fired) {
			//Draw a line from start to end point
			#region Draw Rope
			LineRenderer rope = hook.GetComponent<LineRenderer>();
			rope.SetVertexCount(2);
			rope.SetPosition(0, hookHolder.transform.position);
			rope.SetPosition(1, hook.transform.position);
			#endregion

			if (!hooked) {
				Debug.Log("Fired !hooked: " + hooked);

				#region Throw Hook
				hook.transform.Translate(Vector3.forward * Time.deltaTime * maxHookTravelSpeed);
				currentPlayerDistanceToHook = Vector3.Distance(transform.position, hook.transform.position);
				
				//Return hook if it travels too far
				if (currentPlayerDistanceToHook >= maxHookTravelDistance) {
					ReturnHook();
				}
				#endregion
			}

			else if (hooked) {
				Debug.Log("Fired hooked: " + hooked);

				//Disable gravity
				playerRB.useGravity = false;

				//If player close to destination set reachedDestination to true
				playerDistanceToHook = Vector3.Distance(transform.position, hook.transform.position);
				if (playerDistanceToHook < minPlayerDistanceToDestination) {
					reachedDestination = true;
					//StartCoroutine(ReturnHookWithDelay(0.1f));

					//!Optional: pull up to surface that was grappled
					if (!grounded) {
						//transform.Translate(Vector3.forward * Time.deltaTime * 7f);
						//transform.Translate(Vector3.up * Time.deltaTime * 10f);
					}
				}
				else {
					#region Parent Hook to hooked Object & Move Player to Hook
					//if (!reachedDestination) {
						hook.transform.parent = hookedObject.transform.parent;
						transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * maxPlayerTravelSpeed);	
					//}
					#endregion
				}
			}
		}

		else {
			//NO INPUT: Reparent hook to hookHolder & use gravity
			hook.transform.parent = hookHolder.transform;
			playerRB.useGravity = true;
		}
		#endregion
	}

	#region ReturnHookWithDelay
    IEnumerator ReturnHookWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
		ReturnHook();
    }
	#endregion

	#region ReturnHook
    private void ReturnHook()
    {
		//Reset values
        hook.transform.position = hookHolder.transform.position;
		hook.transform.rotation = hookHolder.transform.rotation;
		fired = false;
		hooked = false;
		reachedDestination = false;

		//Hide rope
		rope.SetVertexCount(0);
    }
	#endregion

	#region CheckGrounded 
	private void CheckGrounded() {
		RaycastHit hit;
		Vector3 dir = new Vector3(0, -1);
		float distance = 1f;

		if (Physics.Raycast(transform.position, dir, out hit, distance)) {
			grounded = true;
		}
		else {
			grounded = false;
		}
	}
	#endregion
}
