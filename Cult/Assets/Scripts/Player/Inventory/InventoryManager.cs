using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory List")]
    public List<ItemType> inventoryList;

    private Dictionary<ItemType, GameObject> activeItems = new Dictionary<ItemType, GameObject>() { };
}
