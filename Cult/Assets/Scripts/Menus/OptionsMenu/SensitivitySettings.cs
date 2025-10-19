using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivitySettings : PlayerSettings
{
    private Slider slider;
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = playerSettingsSO.LookSensitivity;
    }
    public void OnChangeSensitivity()
    {
        playerSettingsSO.LookSensitivity = slider.value;
    }
}
