using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettingsSO : ScriptableObject
{
    public bool EnableScene;
    [Range(0.0001f, 1f)]
    [SerializeField] private float _volumePercentage;
    public float VolumePercentage
    {
        get { return _volumePercentage; }
        set
        {
            if(_volumePercentage != value)
            {
                _volumePercentage = value;
                OnVolumePercentageChange?.Invoke(_volumePercentage);
            }
        }
    }
    public event Action<float> OnVolumePercentageChange;

    
    [Range(0.01f, 100f)]
    public float LookSensitivity = 30f;
}
