using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBubble : MonoBehaviour {
    //This script activates the speech bubbles over the heads of ppl

	void Start () {
        if (GameManager.level != 3 || GameManager.level == 3 && gameObject.name == "RadioBubble")
        {
            disable();
        }        
    }

    public void enable()
    {
        gameObject.SetActive(true);
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }
}
