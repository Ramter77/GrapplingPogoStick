using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cakeIsALie : MonoBehaviour {

    public GameObject fakeCake;
    public GameObject realCake;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cakeLocation")
        {
            fakeCake.SetActive(false);

            realCake.transform.position = fakeCake.transform.position;
            realCake.SetActive(true);
        }
    }
}
