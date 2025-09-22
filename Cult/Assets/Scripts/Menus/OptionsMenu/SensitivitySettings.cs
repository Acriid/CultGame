using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivitySettings : PlayerSettings
{
    private Slider slider;
    [SerializeField] private TMP_InputField inputField;
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = playerSettingsSO.LookSensitivity;
        inputField.text = slider.value.ToString();
    }
    public void OnChangeSensitivity()
    {
        playerSettingsSO.LookSensitivity = slider.value;
        inputField.text = slider.value.ToString();
    }
}
