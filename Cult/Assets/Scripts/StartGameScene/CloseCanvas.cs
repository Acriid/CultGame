using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public GameObject canvas;
    public void OnClick()
    {
        canvas.SetActive(false);
    }
}
