using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Item> _inventoryList;

    private Dictionary<int, GameObject> activeItems = new Dictionary<int, GameObject>() { };

    void OnEnable()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        //WOWOWOW
        int c = 0;
        _inventoryList = FindInventoryItems();
        foreach (Item item in _inventoryList)
        {
            c++;
            if (item.itemSO.IsInInventory)
            {
                activeItems.Add(c, item.gameObject);
                Debug.Log(activeItems[c].name);
            }
        }
    }

    private List<Item> FindInventoryItems()
    {
        IEnumerable<Item> inventoryList = FindObjectsByType<Item>(FindObjectsSortMode.None);
        return new List<Item>(inventoryList);
    }

}
