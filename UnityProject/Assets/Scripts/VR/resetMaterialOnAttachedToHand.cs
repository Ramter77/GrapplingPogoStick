using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class resetMaterialOnAttachedToHand : MonoBehaviour {

    public GameObject handle;
    private MeshRenderer rend;
    private Material handleMat;

    bool reset;

	void Start () {
        rend = handle.GetComponent<MeshRenderer>();
        handleMat = rend.material;
	}
	
    
    public void resetMaterial()
    {
        reset = true;
        
    }

    private void Update()
    {
        if (reset)
        {
            rend.material = handleMat;
        }
    }
}
