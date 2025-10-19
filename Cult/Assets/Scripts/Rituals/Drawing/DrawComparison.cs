using System.Collections.Generic;
using UnityEngine;

public static class DrawComparison
{
    public static bool MeshSimilar2D(GameObject CompareObject, MeshCollider StaticMesh, float threshold = 0.5f, float compareratio = 0.85f)
    {
        int insideCount = 0;
        int closeOutline = 0;
        Mesh CompareMesh = CompareObject.GetComponent<MeshFilter>().mesh;
        Vector3[] compareVertices = CompareMesh.vertices;
        Vector3[] outlineVertices = StaticMesh.sharedMesh.vertices;

        foreach (var point in compareVertices)
        {
            Vector3 worldPoint = CompareObject.transform.TransformPoint(point);
            Vector3 closest = StaticMesh.ClosestPointOnBounds(worldPoint);
            float dist = Vector3.Distance(worldPoint, closest);
            if (dist <= threshold) { insideCount++; }
        }

        foreach (var point in outlineVertices)
        {
            Vector3 worldPoint = StaticMesh.transform.TransformPoint(point);
            Vector3 closest = CompareObject.GetComponent<MeshCollider>().ClosestPoint(worldPoint);
            float dist = Vector3.Distance(worldPoint, closest);
            if (dist > 0f && dist <= threshold) closeOutline++;
        }
        Debug.Log(insideCount);
        float ratio = (float)insideCount / compareVertices.Length;
        float ratioOutline = (float)closeOutline / outlineVertices.Length;
        Debug.Log(ratio > compareratio);
        return ratio > compareratio && ratioOutline > compareratio;
    }

    
}
