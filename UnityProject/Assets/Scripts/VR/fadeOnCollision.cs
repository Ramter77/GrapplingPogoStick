using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class fadeOnCollision : MonoBehaviour {

	private SteamVR_Fade fadeScript;

	private void Start() {
		fadeScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SteamVR_Fade>();
	}

	private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "obstacle") {
            
			fadeScript.OnStartFade(Color.black, 1f, false);
		}
	}

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "obstacle")
        {
            fadeScript.OnStartFade(Color.clear, 1f, false);
        }
    }
    
}
