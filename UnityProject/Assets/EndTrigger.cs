using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {

	public GameControl gm;

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			//gm.StopTimer();
			gm.ended = true;
		}
	}

	// Use this for initialization
	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
