using System.Collections.Generic;
using UnityEngine;

public static class DrawComparison
{
    public static bool MeshSimilar2D(GameObject CompareObject, MeshCollider StaticMesh, float threshold = 0.5f, float compareratio = 0.85f)
    {
        int insideCount = 0;
        Mesh CompareMesh = CompareObject.GetComponent<MeshFilter>().mesh;
        Vector3[] compareVertices = CompareMesh.vertices;
        foreach (var point in compareVertices)
        {
            Vector3 worldPoint = CompareObject.transform.TransformPoint(point);
            Vector3 closest = StaticMesh.ClosestPointOnBounds(worldPoint);
            float dist = Vector3.Distance(worldPoint, closest);
            if (dist <= threshold) { insideCount++; }
        }
        Debug.Log(insideCount);
        float ratio = (float)insideCount / compareVertices.Length;
        Debug.Log(ratio > compareratio);
        return ratio > compareratio;
    }

    
}
