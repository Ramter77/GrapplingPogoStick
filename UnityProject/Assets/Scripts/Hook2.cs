using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Hook2 : MonoBehaviour {
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
	public float maxHookTravelSpeed = 15;
	public float maxHookTravelDistance = 10;
	public float returnSpeed = 10;

	//Private control
	public static bool fired;
	public bool hooked2;
	public GameObject hooked2Object;
	#endregion

	#region Player variables
	[Header ("Player")]
	public float maxPlayerTravelSpeed = 20;
	[Tooltip ("Player is stopped when he is this amount of units away from his destination (hooked2 object)")]
    public float minPlayerDistanceToDestination = 1f;
	private bool grounded, reachedDestination2;
	private float currentPlayerDistanceToHook, playerDistanceToHook;
	private Rigidbody playerRB;
    private bool returning2;

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

	void DrawRope() {
		//Draw a line from start to end point
		#region Draw Rope
		LineRenderer rope = hook.GetComponent<LineRenderer>();
		rope.SetVertexCount(2);
		rope.SetPosition(0, hookHolder.transform.position);
		rope.SetPosition(1, hook.transform.position);
		#endregion
	}

    void Update()
	{
		#region Input
		//Input
		if (!enable_VR) {
			
				if (Input.GetKey(KeyCode.Mouse1)) {
					if (!fired && !returning2) {
						if (Input.GetKeyDown(KeyCode.Mouse1)) {
						fired = true;
						}
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

		if (returning2) {
			ReturnHook();
			DrawRope();
		}

		#region Throw grapple
		if (fired) {
			DrawRope();

			if (!hooked2) {
				Debug.Log("Fired !hooked2: " + hooked2);

				#region Throw Hook
				hook.transform.Translate(Vector3.forward * Time.deltaTime * maxHookTravelSpeed);
				currentPlayerDistanceToHook = Vector3.Distance(transform.position, hook.transform.position);
				
				//Return hook if it travels too far
				if (currentPlayerDistanceToHook >= maxHookTravelDistance) {
					ReturnHook();
				}
				#endregion
			}

			else if (hooked2) {
				Debug.Log("Fired hooked2: " + hooked2);

				//Disable gravity
				playerRB.useGravity = false;

				//If player close to destination set reachedDestination2 to true
				playerDistanceToHook = Vector3.Distance(transform.position, hook.transform.position);
				if (playerDistanceToHook < minPlayerDistanceToDestination) {
					reachedDestination2 = true;
					//StartCoroutine(ReturnHookWithDelay(0.1f));

					//!Optional: pull up to surface that was grappled
					if (!grounded) {
						//transform.Translate(Vector3.forward * Time.deltaTime * 7f);
						//transform.Translate(Vector3.up * Time.deltaTime * 10f);
					}
				}
				else {
					#region Parent Hook to hooked2 Object & Move Player to Hook
					//if (!reachedDestination2) {
						hook.transform.parent = hooked2Object.transform.parent;
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
		returning2 = true;
		if (returning2) {
			//Reset values
			//hook.transform.position = hookHolder.transform.position;
			float step =  returnSpeed * Time.deltaTime; // calculate distance to move
			hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolder.transform.position, step);
			//ansform.position = Vector3.MoveTowards(transform.position, target.position, step);
			hook.transform.rotation = hookHolder.transform.rotation;

			if (hook.transform.position == hookHolder.transform.position) {
				returning2 = false;
				fired = false;
				hooked2 = false;
				reachedDestination2 = false;

				//Hide rope
				rope.SetVertexCount(0);
			}
		}
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
