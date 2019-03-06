using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiveCake : MonoBehaviour {

    public GameManager gm;

    void Start () {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fakeCake")
        {
            Debug.Log("Received fakeCake");

            if (GameManager.activateCake)
            { 
                gm.incrementCake();

                Debug.Log("SEts false" + gameObject);
                gameObject.SetActive(false);                
            }
        }
    }
}
