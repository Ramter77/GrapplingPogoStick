using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSkin : MonoBehaviour {

    public Material[] mat;
    private MeshRenderer meshRendHead;
    private MeshRenderer meshRendBody;

    void Start () {
        meshRendHead = transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<MeshRenderer>();
        meshRendBody = transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>();

        Material applyMat = mat[Random.Range(0, mat.Length)];

        meshRendHead.material = applyMat;
        meshRendBody.material = applyMat;
    }
}
