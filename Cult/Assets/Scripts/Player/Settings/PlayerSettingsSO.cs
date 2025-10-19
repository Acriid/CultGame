using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettingsSO : ScriptableObject
{
    [Range(0f, 1f)]
    public float Volumepercentage = 1f;
    [Range(0.01f, 100f)]
    public float LookSensitivity = 30f;
}
