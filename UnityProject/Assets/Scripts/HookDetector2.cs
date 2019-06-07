using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector2 : MonoBehaviour {

	public GameObject player;

	private void Start() {
		if (!player) {
			player = GameObject.FindGameObjectWithTag("Player");
		}		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 30) {
			player.GetComponent<Hook2>().hooked2 = true;
			player.GetComponent<Hook2>().hooked2Object = other.gameObject;
		}
	}
}