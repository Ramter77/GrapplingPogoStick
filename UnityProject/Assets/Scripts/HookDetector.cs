using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour {

	public GameObject player;

	private void Start() {
		if (!player) {
			player = GameObject.FindGameObjectWithTag("Player");
		}		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Hookable") {
			player.GetComponent<Hook>().hooked = true;
			player.GetComponent<Hook>().hookedObject = other.gameObject;
		}
	}
}
