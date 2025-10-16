using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] public GameObject GameMenu;
    public void OnClick()
    {
        if (GameMenu.activeSelf)
        {
            MenuManager.instance.ChangeMenu(MenuManager.MenuType.None);
        }
    }
}
