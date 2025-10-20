using UnityEngine;
using UnityEngine.InputSystem;

public class CultBook : MonoBehaviour
{
    private InputAction interact;
    public GameObject popUp;
    void OnEnable()
    {
        EnableInput();
        popUp.SetActive(true);
    }
    void OnDisable()
    {
        DisableInput();
        popUp.SetActive(false);
        MenuManager.instance.ChangeMenu(MenuManager.MenuType.None);
    }

    private void EnableInput()
    {
        if (interact == null)
        {
            interact = InputManager.instance.inputActions.Player.Attack;
            interact.performed += OpenBook;
        }
    }
    private void DisableInput()
    {
        if (interact != null)
        {
            interact.performed -= OpenBook;
            interact = null;
        }
    }
    private void OpenBook(InputAction.CallbackContext ctx)
    {
        MenuManager.instance.ChangeMenu(MenuManager.MenuType.Book);
    }
}
