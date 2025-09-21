using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject objectCanvas;
    public void ShowCanvas()
    {
        if (objectCanvas == null) return;
        MenuManager.instance.ReplaceMenu(MenuManager.MenuType.Interactable, objectCanvas);
        MenuManager.instance.ChangeMenu(MenuManager.MenuType.Interactable);  

    }
    public void HideCanvas()
    {
        if (objectCanvas == null) return;
        MenuManager.instance.ChangeMenu(MenuManager.MenuType.None);
    }
}
