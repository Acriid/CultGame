using UnityEngine;
using UnityEngine.UI;

public class SensitivitySettings : PlayerSettings
{
    private Slider slider;
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    public void OnChangeSensitivity()
    {
        playerSettingsSO.LookSensitivity = slider.value;
    }
}
