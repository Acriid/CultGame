using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : PlayerSettings
{
    private Slider slider;
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = playerSettingsSO.VolumePercentage;
    }
    public void OnChangeSound()
    {
        playerSettingsSO.VolumePercentage = slider.value;
    }
}
