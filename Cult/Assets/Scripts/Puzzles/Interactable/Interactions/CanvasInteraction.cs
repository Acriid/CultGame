using UnityEngine;

public class CanvasInteraction : Interactable
{
    [SerializeField] private GameObject objectCanvas;
    public override void Interact()
    {
        if (MenuManager.instance.currentMenu != MenuManager.MenuType.None) { HideCanvas(); }
        else { ShowCanvas(); }
    }
    private void ShowCanvas()
    {
        if (objectCanvas == null) return;
        MenuManager.instance.ReplaceMenu(MenuManager.MenuType.Interactable, objectCanvas);
        MenuManager.instance.ChangeMenu(MenuManager.MenuType.Interactable);  

    }
    private void HideCanvas()
    {
        if (objectCanvas == null) return;
        MenuManager.instance.ChangeMenu(MenuManager.MenuType.None);
    }
}
