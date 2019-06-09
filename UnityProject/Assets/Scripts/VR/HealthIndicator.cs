using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour {

    private static float healthPercentage;
    private static Material material;

	void Start () {
        material = GetComponent<Renderer>().material;
	}

    public void HealthIndicatorUpdate(float hPct) {
        material.SetFloat("_Cutoff", 1f - hPct);        
    }
}
