using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;

//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private List<Item> _inventoryList;
    [Header("Hotbar")]
    [SerializeField] private List<Image> _hotBar;
    [SerializeField] private List<Image> _hotBarImage;
    [SerializeField] private TMP_Text inventoryText;
    [Header("InputAction")]
    private InputAction scrollInput;
    private InputAction inventoryAction;
    [Header("InventorySize Limit")]
    private int hotBarSizeLimit = 4;
    [Header("HoldTransform")]
    public GameObject PickUpHolder;
    public int CurrentSelected = 0;
    private Dictionary<int, GameObject> activeItems = new Dictionary<int, GameObject>() { };
    public static InventoryManager instance { get; private set; }
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryManager.");
        }
        instance = this;
    }
    void OnEnable()
    {
        InitializeInventory();
        InitializeActions();
    }
    void OnDisable()
    {
        CleanUpActions();
        RemoveIninventory();
    }
    void OnDestroy()
    {
        OnDisable();
    }
    #region  All Actions
    #region Initialize Actions
    void InitializeActions()
    {
        InitializeScrollInput();
       // InitializeInventoryAction();
    }
    void CleanUpActions()
    {
        CleanupScrollInput();
        //CleanupInventoryAction();
    }
    #endregion
    #region ScrollWheel
    void InitializeScrollInput()
    {
        if (scrollInput == null)
        {
            scrollInput = InputManager.instance.inputActions.UI.NavigateHotbar;
            scrollInput.performed += ReadScrollValue;
        }
    }
    void CleanupScrollInput()
    {
        if (scrollInput != null)
        {
            scrollInput.performed -= ReadScrollValue;
            scrollInput = null;
        }
    }
    #endregion
    #region Inventory
    private void InitializeInventoryAction()
    {
        if (inventoryAction == null)
        {
            inventoryAction = InputManager.instance.inputActions.Player.Inventory;
            inventoryAction.performed += InventoryAction;
        }
    }
    private void CleanupInventoryAction()
    {
        if (inventoryAction != null)
        {
            inventoryAction.performed -= InventoryAction;
            inventoryAction = null;

        }
    }
    #endregion
    #region InventoryAction
    void InventoryAction(InputAction.CallbackContext ctx)
    {
        OpenInventory(MenuManager.MenuType.Inventory);
    }
    private void OpenInventory(MenuManager.MenuType menuType)
    {
        if (MenuManager.instance.currentMenu == menuType)
        {
            MenuManager.instance.ChangeMenu(MenuManager.MenuType.None);
        }
        else
        {
            MenuManager.instance.ChangeMenu(menuType);
        }
    }
    #endregion
    #endregion
    private void InitializeInventory()
    {
        int loopvariable = 0;
        _inventoryList = FindInventoryItems();
        for (int i = 0; i <= hotBarSizeLimit; i++)
        {
            activeItems.Add(i, null);
        }
        
        foreach (Item item in _inventoryList)
        {
            if (activeItems[loopvariable] == null && item.itemSO.IsInInventory)
            {
                InteractMechanic.instance.PickUpItem(item.gameObject);
                AddtoInventory(item);
            }
            loopvariable++;
        }



        changeText();
    }


    public void AddtoInventory(Item addingItem)
    {
        string DebugMessage = "Inventory Full";

        for (int i = 0; i <= hotBarSizeLimit; i++)
        {
            if(activeItems[CurrentSelected] != null)
            {
                activeItems[CurrentSelected].SetActive(false);
                addingItem.itemSO.IsEquiped = false;
            }
            if (activeItems[i] == null)
            {
                DebugMessage = "Added Item to Inventory";
                addingItem.itemSO.IsInInventory = true;
                if (addingItem.itemSO.PritoryItem) { CurrentSelected = hotBarSizeLimit; }
                else{CurrentSelected = i;}
                _hotBarImage[CurrentSelected].sprite = addingItem.itemSO.ItemSprite;
                ChangeColor(true);
                activeItems[CurrentSelected] = addingItem.gameObject;
                changeText();
                addingItem.itemSO.IsEquiped = true;
                break;
            }
            
        }
        Debug.Log(DebugMessage);

    }

    public void RemoveFromInventory()
    {
        activeItems.TryGetValue(CurrentSelected, out GameObject result);
        if (result == null)
        {
            return;
        }
        if (activeItems.ContainsKey(CurrentSelected))
        {
            activeItems[CurrentSelected].GetComponent<Item>().itemSO.IsInInventory = false;
            activeItems[CurrentSelected] = null;
            _hotBarImage[CurrentSelected].sprite = null;
            ChangeColor(false);
        }
        changeText();

    }

    private void ReadScrollValue(InputAction.CallbackContext ctx)
    {
        Vector2 ScrollValue = ctx.ReadValue<Vector2>();
        ChangeSelectedItem((int)ScrollValue.y);
    }
    private List<Item> FindInventoryItems()
    {
        IEnumerable<Item> inventoryList = FindObjectsByType<Item>(FindObjectsSortMode.None);
        return new List<Item>(inventoryList);
    }

    private void changeText()
    {
        if (activeItems[CurrentSelected] != null)
        {
            inventoryText.text = activeItems[CurrentSelected].name;
        }
        else
        {
            inventoryText.text = "Empty";
        }
    }

    public bool InventoryFull()
    {
        for (int i = 0; i < hotBarSizeLimit; i++)
        {
            if (activeItems[i] == null) return false;
        }
        return true;
    }

    private void ChangeSelectedItem(int changeValue)
    {
        activeItems.TryGetValue(CurrentSelected, out GameObject result);

        if (CurrentSelected != hotBarSizeLimit) { _hotBar[CurrentSelected].color = Color.white; }
        else { _hotBar[CurrentSelected].color = Color.green; }

        if (result != null)
        {
            activeItems[CurrentSelected].SetActive(false);
            activeItems[CurrentSelected].GetComponent<Item>().itemSO.IsEquiped = false;
            _hotBar[CurrentSelected].color = Color.white;
        }

        CurrentSelected += changeValue;
        CurrentSelected = Mathf.Clamp(CurrentSelected, 0, hotBarSizeLimit);

        if (!activeItems.ContainsKey(CurrentSelected))
        {
            CurrentSelected -= changeValue;
        }

        activeItems.TryGetValue(CurrentSelected, out result);

        if (result != null)
        {
            activeItems[CurrentSelected].SetActive(true);
            activeItems[CurrentSelected].GetComponent<Item>().itemSO.IsEquiped = true;
        }
            
        InteractMechanic.instance.SetCarryItem(false);
            
        _hotBar[CurrentSelected].color = Color.blue;

        if (activeItems[CurrentSelected] != null)
        {
            InteractMechanic.instance.SetCarryItem(true);
        }
        else
        {
            InteractMechanic.instance.SetCarryItem(false);
        }

        InteractMechanic.instance.SetCurrentSelected(activeItems[CurrentSelected]);
        changeText();
    }
    
    public GameObject getCurrentHeldObject()
    {
        return activeItems[CurrentSelected];
    }
    private void ChangeColor(bool value)
    {
        _hotBarImage[CurrentSelected].enabled = value;
    }
    private void RemoveIninventory()
    {
        foreach(Item item in _inventoryList)
        {
            item.itemSO.IsInInventory = false;
        }
    }
}
