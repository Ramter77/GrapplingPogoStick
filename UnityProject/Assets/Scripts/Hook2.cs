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
	private bool grounded, reachedDestination;
	private float currentPlayerDistanceToHook, playerDistanceToHook;
	private Rigidbody playerRB;
    private bool returning;
    private bool rayHit;
    private Vector3 pos;
    private bool moving;
    public LayerMask layerMask;

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
					if (!fired && !returning) {
						if (Input.GetKeyDown(KeyCode.Mouse1)) {
							fired = true;
							rayHit = false;
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

		if (returning) {
			ReturnHook();
			DrawRope();
		}
		else
		{
			if (fired && moving) {
				hook.transform.position = Vector3.MoveTowards(hook.transform.position, pos, Time.deltaTime * maxHookTravelSpeed);
			}
		}

		#region Throw grapple
		if (fired) {
			DrawRope();

			hook.transform.parent = null;

			if (!hooked2) {
				Debug.Log("Fired !hooked2: " + hooked2);

				#region Throw Hook
				//hook.transform.Translate(Vector3.forward * Time.deltaTime * maxHookTravelSpeed);


				RaycastHit hit;
				// Does the ray intersect any objects excluding the player layer
				if (!rayHit && !moving) {
					if (Physics.Raycast(hookHolder.transform.position, hookHolder.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
					{
						if (!moving && !returning && !rayHit) {
						rayHit = true;
						pos = hit.point;
						//hook.transform.Translate(Vector3.forward * Time.deltaTime * maxHookTravelSpeed);

						moving = true;
						}
						

						//hook.GetComponent<Rigidbody>().AddForce(pos - hook.transform.position, ForceMode.Impulse);
					}
				else
				{
					hook.transform.Translate(Vector3.forward * Time.deltaTime * maxHookTravelSpeed);
					//rayHit = false;
					//hook.transform.position = Vector3.MoveTowards(hook.transform.position, pos, maxHookTravelDistance);
					Debug.Log("EAFKEUABZfkziizzhiz");
					//hook.transform.Translate(Vector3.forward * Time.deltaTime * maxHookTravelSpeed);

					//ReturnHook();
					//hook.transform.position = hookHolder.transform.position;
				}
				}
				
				//hook.transform.position = Vector3.MoveTowards(hook.transform.position, );
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
				//playerRB.useGravity = false;

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
					#region Parent Hook to hooked2 Object & Move Player to Hook
					//if (!reachedDestination) {
						hook.transform.parent = hooked2Object.transform.parent;
						playerRB.AddForce((hook.transform.position-hookHolder.transform.position).normalized * Time.deltaTime * maxPlayerTravelSpeed, ForceMode.Impulse);
						//transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * maxPlayerTravelSpeed);	
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
		returning = true;
		if (returning) {
			//Reset values
			//hook.transform.position = hookHolder.transform.position;
			float step =  returnSpeed * Time.deltaTime; // calculate distance to move
			hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolder.transform.position, step);
			//ansform.position = Vector3.MoveTowards(transform.position, target.position, step);
			hook.transform.rotation = hookHolder.transform.rotation;

			if (hook.transform.position == hookHolder.transform.position) {
				returning = false;
				fired = false;
				hooked2 = false;
				reachedDestination = false;
				moving = false;

				hook.transform.parent = hookHolder.transform;

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

	private void OnDrawGizmos() {
		Debug.DrawRay(hookHolder.transform.position, hookHolder.transform.TransformDirection(Vector3.forward) * maxHookTravelDistance, Color.red);
	}
}
