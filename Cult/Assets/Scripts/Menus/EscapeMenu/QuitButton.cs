using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnClick()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }
}
