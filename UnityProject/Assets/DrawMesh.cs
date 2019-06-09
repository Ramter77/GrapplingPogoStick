using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawMesh : MonoBehaviour {

	public Mesh mesh;
	public int subMeshes;
	public Color color1, color2, color3;
    public Material material;
    private MaterialPropertyBlock block;
    private int colorID;

    void Start()
    {
        block = new MaterialPropertyBlock();
        colorID = Shader.PropertyToID("_Color");
    }

    void Update()
    {
        block.SetColor(colorID, color1);
        Graphics.DrawMesh(mesh, transform.position, transform.rotation, material, 0, null, 0, block);

		if (subMeshes == 1) {
			block.SetColor(colorID, color2);
			Graphics.DrawMesh(mesh, transform.position, transform.rotation, material, 0, null, 1, block);
		}

		if (subMeshes == 2) {
			block.SetColor(colorID, color3);
			Graphics.DrawMesh(mesh, transform.position, transform.rotation, material, 0, null, 2, block);
		}
    }
}
