using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : PlayerSettings
{
    private Slider slider;
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = playerSettingsSO.Volumepercentage;
    }
    public void OnChangeSound()
    {
        playerSettingsSO.Volumepercentage = slider.value;
    }
}
