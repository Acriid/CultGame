using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject objectCanvas;
    public bool CanvasShown()
    {
        if (objectCanvas.activeSelf) return true;
        return false;
    }
    public void ShowCanvas()
    {
        if (objectCanvas == null) return;
        objectCanvas.SetActive(true);
    }
    public void HideCanvas()
    {
        if (objectCanvas == null) return;
        objectCanvas.SetActive(false);
    }
}
