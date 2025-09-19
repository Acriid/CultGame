using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject objectCanvas;
    public void ShowCanvas()
    {
        MenuManager.instance.ReplaceMenu(MenuManager.MenuType.Interactable, objectCanvas);
        MenuManager.instance.ChangeMenu(MenuManager.MenuType.Interactable);
    }
    public void HideCanvas()
    {
        MenuManager.instance.ChangeMenu(MenuManager.MenuType.None);
    }
}
