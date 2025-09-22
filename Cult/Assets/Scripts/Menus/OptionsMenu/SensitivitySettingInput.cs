using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivitySettingInput : PlayerSettings
{
    [SerializeField] private Slider slider;
    private TMP_InputField inputField;
    void Awake()
    {
        inputField = gameObject.GetComponent<TMP_InputField>();
        slider.value = playerSettingsSO.LookSensitivity;
        inputField.text = slider.value.ToString();
    }
    public void OnChangeSensitivity()
    {
        playerSettingsSO.LookSensitivity = int.Parse(inputField.text);
        slider.value = int.Parse(inputField.text);
    }
}
