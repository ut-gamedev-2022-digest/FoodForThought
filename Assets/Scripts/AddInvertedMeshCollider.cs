using System.Linq;
using UnityEngine;

public class AddInvertedMeshCollider : MonoBehaviour
{
    public bool removeExistingColliders = true;
    public bool isTrigger;

    private void Start()
    {
        CreateInvertedMeshCollider();
    }

    private void CreateInvertedMeshCollider()
    {
        if (removeExistingColliders) RemoveExistingColliders();

        InvertMesh();

        gameObject.AddComponent<MeshCollider>();

        if (!isTrigger) return;
        var meshCollider = GetComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.isTrigger = isTrigger;
    }

    private void RemoveExistingColliders()
    {
        var colliders = GetComponents<Collider>();
        foreach (var t in colliders)
            DestroyImmediate(t);
    }

    private void InvertMesh()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }
}