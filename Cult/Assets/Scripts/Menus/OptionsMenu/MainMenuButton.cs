using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    public void OnClick()
    {
       if(!mainMenu.activeSelf) mainMenu.SetActive(true);
       if(optionsMenu.activeSelf) optionsMenu.SetActive(false);
    }
}
