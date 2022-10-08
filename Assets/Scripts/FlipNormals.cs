using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipNormals : MonoBehaviour
{
    private void Start()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        var normals = mesh.normals;
        for (var i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        
        mesh.normals = normals;
        
        for (var i = 0; i < mesh.subMeshCount; i++)
        {
            var triangles = mesh.GetTriangles(i);
            for (var j = 0; j < triangles.Length; j += 3)
            {
                (triangles[j + 0], triangles[j + 1]) = (triangles[j + 1], triangles[j + 0]);
            }
            mesh.SetTriangles(triangles, i);
        }
    }
}
