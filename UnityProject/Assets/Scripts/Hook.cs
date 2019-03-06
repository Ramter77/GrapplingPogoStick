using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
	public GameObject hook;
	public GameObject hookHolder;

	public float hookTravelSpeed;
	public float playerTravelSpeed;

	public static bool fired;
	public bool hooked;
	public GameObject hookedObject;

	public float maxDistance;
	private float currentDistance;
    private bool grounded;

    void Update()
	{
		//Input
		if (Input.GetMouseButtonDown(0) && !fired) {
			fired = true;
		}

		if (fired) {
			LineRenderer rope = hook.GetComponent<LineRenderer>();
			rope.SetVertexCount(2);
			rope.SetPosition(0, hookHolder.transform.position);
			rope.SetPosition(1, hook.transform.position);
		}

		if (fired && !hooked) {
			//Move player
			hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
			currentDistance = Vector3.Distance(transform.position, hook.transform.position);
		
			if (currentDistance >= maxDistance) {
				ReturnHook();
			}
		}

		if (fired && hooked) {
			hook.transform.parent = hookedObject.transform.parent;
			transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
			float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);
		
			GetComponent<Rigidbody>().useGravity = false;

			if (distanceToHook < 1) {
				//ReturnHook();

				if (!grounded) {
					transform.Translate(Vector3.forward * Time.deltaTime * 7f);
					transform.Translate(Vector3.up * Time.deltaTime * 10f);
				}

				StartCoroutine(Climb());
			}
		}
		else {
			hook.transform.parent = hookHolder.transform;
			GetComponent<Rigidbody>().useGravity = true;
		}
	}

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
		ReturnHook();
    }

    private void ReturnHook()
    {
        hook.transform.position = hookHolder.transform.position;
		hook.transform.rotation = hookHolder.transform.rotation;
		fired = false;
		hooked = false;

		LineRenderer rope = hook.GetComponent<LineRenderer>();
		rope.SetVertexCount(0);
    }

	private void CheckGrounded() {
		RaycastHit hit;
		float distance = 1f;

		Vector3 dir = new Vector3(0, -1);

		if (Physics.Raycast(transform.position, dir, out hit, distance)) {
			grounded = true;
		}
		else {
			grounded = false;
		}
	}
}
