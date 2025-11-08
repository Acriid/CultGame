using UnityEngine;
using UnityEngine.UI;

public class DisableScene : MonoBehaviour
{
    public PlayerSettingsSO playerSettingsSO;
    void OnEnable()
    {
        this.GetComponent<Toggle>().isOn = playerSettingsSO.EnableScene;
    }
    public void OnToggleChange()
    {
        playerSettingsSO.EnableScene = this.GetComponent<Toggle>().isOn;
    }
}
