using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    public void OnClick()
    {
       if(optionsMenu.activeSelf) optionsMenu.SetActive(false);
       if(!mainMenu.activeSelf) mainMenu.SetActive(true);
    }
}
