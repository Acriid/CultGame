using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    private List<Item> _inventoryList;
    [Header("InputAction")]
    public InputActionReference scrollInput;
    [Header("InventorySize Limit")]
    public int inventorysizeLimit = 4;
    private int CurrentSelected = 0;
    private Dictionary<int, GameObject> activeItems = new Dictionary<int, GameObject>() { };

    void OnEnable()
    {
        InitializeInventory();
        scrollInput.action.performed += ReadScrollValue;
    }
    void OnDisable()
    {
        scrollInput.action.performed -= ReadScrollValue;
    }
    private void InitializeInventory()
    {
        int loopvariable = 0;
        _inventoryList = FindInventoryItems();
        foreach (Item item in _inventoryList)
        {
            if (item.itemSO.IsInInventory)
            {
                activeItems.Add(loopvariable, item.gameObject);
                Debug.Log(activeItems[loopvariable].name);
                loopvariable++;
            }
        }
    }

    private List<Item> FindInventoryItems()
    {
        IEnumerable<Item> inventoryList = FindObjectsByType<Item>(FindObjectsSortMode.None);
        return new List<Item>(inventoryList);
    }
    public void AddtoInventory(Item addingItem)
    {
        string DebugMessage = "Inventory Full";
        for (int i = 0; i < inventorysizeLimit; i++)
        {
            if (activeItems[i] == null)
            {
                DebugMessage = "Added Item to Inventory";
                addingItem.itemSO.IsInInventory = true;
                CurrentSelected = i;
                activeItems.Add(CurrentSelected, addingItem.gameObject);
            }
        }
        Debug.Log(DebugMessage);
        
        
    }
    public void RemoveFromInventory()
    {
        activeItems[CurrentSelected].GetComponent<Item>().itemSO.IsInInventory = false;
        activeItems.Remove(CurrentSelected);
    }

    private void ReadScrollValue(InputAction.CallbackContext ctx)
    {
        Vector2 ScrollValue = ctx.ReadValue<Vector2>();
        activeItems[CurrentSelected].gameObject.SetActive(false);
        CurrentSelected += (int)ScrollValue.y;
        CurrentSelected = Mathf.Clamp(CurrentSelected, 0, 3);
        if (!activeItems.ContainsKey(CurrentSelected))
        {
            CurrentSelected -= (int)ScrollValue.y;
        }
        activeItems[CurrentSelected].gameObject.SetActive(true);
        Debug.Log(CurrentSelected);
    }
    //759127



}
