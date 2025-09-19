using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }
    public MenuType currentMenu { get; set; } = MenuType.None;
    [SerializeField] NewDictionary newDictionary;
    
    public Dictionary<MenuType, GameObject> menus;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of MenuManager.");
        }
        instance = this;
        menus = newDictionary.toDictionary();
        menus.Add(currentMenu, null);
    }
    public void ChangeMenu(MenuType menuType)
    {
        if (menus == null)
        {
            Debug.Log("Something went wrong");
            return;
        }
        Debug.Log("Called ChangeMenu");
        if (!menus.ContainsKey(menuType))
        {
            Debug.Log("Added Menu");
            menus.Add(menuType, null);
        }
        Debug.Log(menuType);
        Debug.Log(currentMenu);
        if (menuType == MenuType.None || currentMenu == menuType || (currentMenu != MenuType.None && menuType != MenuType.None))
        {
            Debug.Log("Clossing Current Menu");
            if (menus[currentMenu] != null)
            {
                menus[currentMenu].SetActive(false);
                currentMenu = MenuType.None;
            }
        }
        else
        {
            Debug.Log("Opening new Menu");
            menus[menuType].SetActive(true);
            currentMenu = menuType;
        }
        
        
    }
    public void ReplaceMenu(MenuType menuType, GameObject menuObject)
    {
        menus[menuType] = menuObject;
    }
    public enum MenuType
    {
        Interactable,
        Settings,
        Inventory,
        None
    }
}
[Serializable]
public class NewDictionary
{
    [SerializeField] NewDictionaryItem[] newDictionaryItems;
    public Dictionary<MenuManager.MenuType, GameObject> toDictionary()
    {
        Dictionary<MenuManager.MenuType, GameObject> newDictionary = new Dictionary<MenuManager.MenuType, GameObject>();
        foreach (var item in newDictionaryItems)
        {
            newDictionary.Add(item.type, item.menuObject);
        }
        return newDictionary;
    }
}
[Serializable]
public class NewDictionaryItem
{
    [SerializeField] public MenuManager.MenuType type;
    [SerializeField] public GameObject menuObject;
}
