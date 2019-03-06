using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomCharacter : MonoBehaviour {

    public bool includeMesh;

    public Mesh[] mesh;
    public Material[] mat;

    private MeshFilter meshFilter;
    private MeshRenderer meshRend;

	void Start () {
        if (includeMesh)
        {
            meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = mesh[Random.Range(0, mesh.Length)];
        }
        
        meshRend = GetComponent<MeshRenderer>();        
        meshRend.material = mat[Random.Range(0, mat.Length)];
    }
}
