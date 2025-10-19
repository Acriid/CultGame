using UnityEngine;

public class TestPoints : Interactable
{
    public GameObject drawing;
    public MeshCollider outline;
    public float threshold;
    public float ratio;
    public override void Interact()
    {
        DrawComparison.MeshSimilar2D(drawing, outline, threshold, ratio);
    }
}
