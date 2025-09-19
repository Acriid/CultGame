using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
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
        menus = newDictionary.toDictionary();
    }
    public void ChangeMenu(MenuType menuType)
    {
        if (currentMenu != menuType)
        {
            menus[currentMenu].SetActive(false);
        }
        else
        {
            menus[menuType].SetActive(true);
        }
        currentMenu = menuType;
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
