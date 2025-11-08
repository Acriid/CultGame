using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[CustomEditor(typeof(AiDetection))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        AiDetection aiDetection = (AiDetection)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(aiDetection.transform.position, Vector3.up, Vector3.forward, 360, aiDetection.radius);

        Vector3 viewAngle1 = DirectionFromAngle(aiDetection.transform.eulerAngles.y, -aiDetection.angle / 2f);
        Vector3 viewAngle2 = DirectionFromAngle(aiDetection.transform.eulerAngles.y, aiDetection.angle / 2f);

        Handles.color = Color.yellow;
        Handles.DrawLine(aiDetection.transform.position, aiDetection.transform.position + viewAngle1 * aiDetection.radius);
        Handles.DrawLine(aiDetection.transform.position, aiDetection.transform.position + viewAngle2 * aiDetection.radius);

        if(aiDetection.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(aiDetection.transform.position, aiDetection.playerRef.transform.position);
        }
    }
    
    private Vector3 DirectionFromAngle(float eulerY,float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
