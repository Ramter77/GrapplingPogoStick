using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigarette : MonoBehaviour {
	//This script handles the emissive material over time on the cigarette

	[Header ("Material properties")]
	public Material mLightMaterial;
	public float mMin = 0.1f;
	public float mMax = 1f;

	void Start () {
		SetLightColor(mMin);
	}

	public IEnumerator Drag(Head head) {		
		float emission = mMin;
		float time = 0;

		while (gameObject.activeSelf) {
			//Target brightness
			float brightness = head.TestForAudioInput() ? mMax : mMin;

			//Lerp
			time += Time.deltaTime * 0.01f;
			emission = Mathf.Lerp(emission, brightness, time);

			//Set
			SetLightColor(emission);
			yield return null;
		}		
	}

	private void SetLightColor(float emission) {
		//Figure out target color
		Color baseColor = mLightMaterial.color;
		Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
		
		//Set in material
		mLightMaterial.SetColor("_EmissionColor", finalColor);
	}
}
