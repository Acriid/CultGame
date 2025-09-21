using System.Collections.Generic;
using UnityEngine;

public static class DrawComparison
{
    public static bool MeshSimilar2D(Mesh CompareMesh, MeshCollider StaticMesh, float threshold = 0.5f, float compareratio = 0.85f)
    {
        int insideCount = 0;
        Vector3[] compareVertices = CompareMesh.vertices;

        foreach (var point in compareVertices)
        {
            Vector3 worldPoint = StaticMesh.transform.TransformPoint(point);
            Vector3 closest = StaticMesh.ClosestPoint(worldPoint);
            float dist = Vector3.Distance(worldPoint, closest);

            if (dist <= threshold) insideCount++;
        }

        float ratio = (float)insideCount / compareVertices.Length;
        return ratio > compareratio;
    }

    
}
