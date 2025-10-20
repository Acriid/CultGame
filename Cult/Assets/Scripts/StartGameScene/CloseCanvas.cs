using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvas2;
    public void OnClick()
    {
        canvas.SetActive(false);
        canvas2.SetActive(true);
    }
}
