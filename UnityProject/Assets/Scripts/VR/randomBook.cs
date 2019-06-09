using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBook : MonoBehaviour {

    public Material[] mat;
    private MeshRenderer meshRend;

    void Start()
    {
        meshRend = transform.GetChild(0).GetComponent<MeshRenderer>();

        Material applyMat = mat[Random.Range(0, mat.Length)];

        meshRend.material = applyMat;
    }
}
