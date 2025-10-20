using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    public PlayerSettingsSO playerSettingsSO;
    void OnEnable()
    {
        playerSettingsSO.OnVolumePercentageChange += SetMasterVolume;
        SetMasterVolume(playerSettingsSO.VolumePercentage); 
    }
    void OnDisable()
    {
        playerSettingsSO.OnVolumePercentageChange -= SetMasterVolume;
    }
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level)*20f);
    }
}
