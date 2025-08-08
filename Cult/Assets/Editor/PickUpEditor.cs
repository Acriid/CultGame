using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PickUpMechanic))]
public class PickUpEditor : Editor
{
    void OnSceneGUI()
    {
       // PickUpMechanic pum = (PickUpMechanic)target;
        //Handles.color = Color.magenta;
        //Handles.DrawLine(pum.Camera.position, pum.Camera.forward * pum.CheckLength);
    }
}
