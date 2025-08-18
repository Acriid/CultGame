using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Item> _inventoryList;
    private int InventorySize = 0;
    private int CurrentSelected;
    private Dictionary<int, GameObject> activeItems = new Dictionary<int, GameObject>() { };

    void OnEnable()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        _inventoryList = FindInventoryItems();
        foreach (Item item in _inventoryList)
        {
            if (item.itemSO.IsInInventory)
            {
                InventorySize++;
                activeItems.Add(InventorySize, item.gameObject);
                Debug.Log(activeItems[InventorySize].name);
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
        addingItem.itemSO.IsInInventory = true;
        InventorySize++;
        activeItems.Add(InventorySize, addingItem.gameObject);
    }
    public void RemoveFromInventory()
    {
        activeItems[CurrentSelected].GetComponent<Item>().itemSO.IsInInventory = false;
        InventorySize--;
        activeItems.Remove(CurrentSelected);
    }

}
