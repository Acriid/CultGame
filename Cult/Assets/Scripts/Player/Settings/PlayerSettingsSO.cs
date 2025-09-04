using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettingsSO : ScriptableObject
{
    [Range(0f, 100f)]
    [SerializeField] private float Volumepercentage = 100f;
    [Range(0.01f, 1000f)]
    [SerializeField] private float LookSensitivity = 30f;
    
}
