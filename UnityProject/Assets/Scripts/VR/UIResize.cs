using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResize : MonoBehaviour {


    public static bool resizeCanvas;
    public GameObject canvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        /*
        if (resizeCanvas == true)
        {

            IncreaseUI();

        }
        else {

        }
        */
	}

    public void IncreaseUI() {

        canvas.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
        //Debug.Log("yes");

        //Time.timeScale = 0.2f;


    }

    public void DecreaseUI()
    {

        canvas.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //Debug.Log("no");

        //Time.timeScale = 1f;
    }



}
