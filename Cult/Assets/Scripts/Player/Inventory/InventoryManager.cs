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
    [Header("HoldTransform")]
    public GameObject PickUpHolder;
    private PickUpMechanic pickUpMechanic;
    private int CurrentSelected = 0;
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
        pickUpMechanic = PickUpHolder.GetComponent<PickUpMechanic>();
        foreach (Item item in _inventoryList)
        {
            if (item.itemSO.IsInInventory)
            {
                activeItems.Add(loopvariable, item.gameObject);
                pickUpMechanic.PickUpItem(item.gameObject);
                if (loopvariable != CurrentSelected)
                {
                    activeItems[loopvariable].SetActive(false);
                }
                else
                {
                    pickUpMechanic.SetCurrentSelected(item.gameObject);
                }
                Debug.Log(activeItems[loopvariable].name);
                loopvariable++;
            }
        }
        for (int i = 0; i <= inventorysizeLimit; i++)
        {
            if (!activeItems.ContainsKey(i))
            {
                activeItems.Add(i, null);
            }
        }
    }

    
    public void AddtoInventory(Item addingItem)
    {
        string DebugMessage = "Inventory Full";
        for (int i = 0; i <= inventorysizeLimit; i++)
        {
            if (activeItems[i] == null)
            {
                DebugMessage = "Added Item to Inventory";
                addingItem.itemSO.IsInInventory = true;
                CurrentSelected = i;
                activeItems[CurrentSelected] = addingItem.gameObject;
               // pickUpMechanic.SetCurrentSelected(addingItem.gameObject);
            }
        }
        Debug.Log(DebugMessage);
    }

    public void RemoveFromInventory()
    {
        activeItems.TryGetValue(CurrentSelected, out GameObject result);
        if (result != null)
        {
            return;
        }
        if (activeItems.ContainsKey(CurrentSelected))
        {
            activeItems[CurrentSelected].GetComponent<Item>().itemSO.IsInInventory = false;
            activeItems.Remove(CurrentSelected);
        }

    }

    private void ReadScrollValue(InputAction.CallbackContext ctx)
    {
        Vector2 ScrollValue = ctx.ReadValue<Vector2>();
        activeItems.TryGetValue(CurrentSelected, out GameObject result);
        if (activeItems.ContainsKey(CurrentSelected) && result != null)
        {
            activeItems[CurrentSelected].SetActive(false);
        }
        CurrentSelected += (int)ScrollValue.y;
        CurrentSelected = Mathf.Clamp(CurrentSelected, 0, inventorysizeLimit);
        if (!activeItems.ContainsKey(CurrentSelected))
        {
            CurrentSelected -= (int)ScrollValue.y;
        }
        activeItems.TryGetValue(CurrentSelected, out result);
        if (activeItems.ContainsKey(CurrentSelected))
        {
            if (result != null)
            {
                activeItems[CurrentSelected].SetActive(true);
            }
            pickUpMechanic.SetCurrentSelected(activeItems[CurrentSelected]);
            pickUpMechanic.SetCarryItem(false);
        }
        Debug.Log(activeItems[CurrentSelected]);
    }
    //759127
    private List<Item> FindInventoryItems()
    {
        IEnumerable<Item> inventoryList = FindObjectsByType<Item>(FindObjectsSortMode.None);
        return new List<Item>(inventoryList);
    }



}
