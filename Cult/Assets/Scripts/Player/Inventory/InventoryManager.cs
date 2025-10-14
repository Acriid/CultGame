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
    [SerializeField] private TMP_Text inventoryText;
    [Header("InputAction")]
    private InputAction scrollInput;
    private InputAction inventoryAction;
    [Header("InventorySize Limit")]
    private int hotBarSizeLimit = 4;
    [Header("HoldTransform")]
    public GameObject PickUpHolder;
    private InteractMechanic interactMechanic;
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
        InitializeInventoryAction();
    }
    void CleanUpActions()
    {
        CleanupScrollInput();
        CleanupInventoryAction();
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
        GameObject pritoryItem = null;
        int loopvariable = 0;
        _inventoryList = FindInventoryItems();
        interactMechanic = PickUpHolder.GetComponent<InteractMechanic>();

        foreach (Item item in _inventoryList)
        {
            if (item.itemSO.PritoryItem && item.itemSO.IsInInventory)
            {
                pritoryItem = item.gameObject;
                continue;
            }
            if (item.itemSO.IsInInventory)
            {
                activeItems.Add(loopvariable, item.gameObject);
                interactMechanic.PickUpItem(item.gameObject);
                if (loopvariable != CurrentSelected)
                {
                    activeItems[loopvariable].SetActive(false);
                }
                else
                {
                    interactMechanic.SetCurrentSelected(item.gameObject);
                }
                _hotBar[loopvariable].color = Color.blue;
                loopvariable++;
            }
        }

        for (int i = 0; i <=  hotBarSizeLimit; i++)
        {
            if (!activeItems.ContainsKey(i))
            {
                if (i == hotBarSizeLimit && pritoryItem != null)
                {
                    activeItems.Add(i, pritoryItem);
                    interactMechanic.PickUpItem(pritoryItem);
                    _hotBar[i].color = Color.black;
                    CurrentSelected = hotBarSizeLimit;
                    break;
                }
                activeItems.Add(i, null);
            }
        }

        _hotBar[CurrentSelected].color = Color.red;
        if (!(activeItems.ContainsKey(hotBarSizeLimit) && activeItems[hotBarSizeLimit] != null))
        {
            _hotBar[hotBarSizeLimit].color = Color.grey;
        }

        changeText();
    }


    public void AddtoInventory(Item addingItem)
    {
        string DebugMessage = "Inventory Full";

        for (int i = 0; i <= hotBarSizeLimit; i++)
        {
            if (addingItem.itemSO.PritoryItem)
            {
                DebugMessage = "Added Item to Inventory";
                _hotBar[CurrentSelected].color = Color.white;
                addingItem.itemSO.IsInInventory = true;
                CurrentSelected = hotBarSizeLimit;
                activeItems[CurrentSelected] = addingItem.gameObject;
                _hotBar[CurrentSelected].color = Color.black;
                changeText();
                break;
            }

            if (activeItems[i] == null)
            {

                DebugMessage = "Added Item to Inventory";
                _hotBar[CurrentSelected].color = Color.white;
                addingItem.itemSO.IsInInventory = true;
                CurrentSelected = i;
                _hotBar[CurrentSelected].color = Color.red;
                activeItems[CurrentSelected] = addingItem.gameObject;
                changeText();
                break;
                // interactMechanic.SetCurrentSelected(addingItem.gameObject);
            }
            addingItem.itemSO.IsEquiped = true;
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
        else { _hotBar[CurrentSelected].color = Color.grey; }
        
        if (activeItems.ContainsKey(CurrentSelected) && result != null)
        {
            activeItems[CurrentSelected].SetActive(false);
            activeItems[CurrentSelected].GetComponent<Item>().itemSO.IsEquiped = false;
            if (CurrentSelected != hotBarSizeLimit) { _hotBar[CurrentSelected].color = Color.blue; }
            else { _hotBar[CurrentSelected].color = Color.black; }
            

        }

        CurrentSelected += changeValue;
        CurrentSelected = Mathf.Clamp(CurrentSelected, 0, hotBarSizeLimit);

        if (!activeItems.ContainsKey(CurrentSelected))
        {
            CurrentSelected -= changeValue;
        }

        activeItems.TryGetValue(CurrentSelected, out result);

        if (activeItems.ContainsKey(CurrentSelected))
        {
            if (result != null)
            {
                activeItems[CurrentSelected].SetActive(true);
                activeItems[CurrentSelected].GetComponent<Item>().itemSO.IsEquiped = true;
            }
            interactMechanic.SetCarryItem(false);
        }
        _hotBar[CurrentSelected].color = Color.red;

        if (activeItems[CurrentSelected] != null)
        {
            interactMechanic.SetCarryItem(true);
        }
        else
        {
            interactMechanic.SetCarryItem(false);
        }

        interactMechanic.SetCurrentSelected(activeItems[CurrentSelected]);
        changeText();
    }

}
