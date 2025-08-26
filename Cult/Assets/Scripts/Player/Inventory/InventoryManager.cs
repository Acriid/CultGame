using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private List<Item> _inventoryList;
    [Header("Hotbar Images")]
    [SerializeField] private List<Image> _hotBar;
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
                    _hotBar[CurrentSelected].color = Color.blue;
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
        _hotBar[CurrentSelected].color = Color.red;
    }


    public void AddtoInventory(Item addingItem)
    {
        string DebugMessage = "Inventory Full";
        
        for (int i = 0; i <= inventorysizeLimit; i++)
        {
            if (activeItems[i] == null)
            {

                DebugMessage = "Added Item to Inventory";
                _hotBar[CurrentSelected].color = Color.white;
                addingItem.itemSO.IsInInventory = true;
                CurrentSelected = i;
                _hotBar[CurrentSelected].color = Color.red;
                activeItems[CurrentSelected] = addingItem.gameObject;
                break;
                // pickUpMechanic.SetCurrentSelected(addingItem.gameObject);
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
        }

    }

    private void ReadScrollValue(InputAction.CallbackContext ctx)
    {
        Vector2 ScrollValue = ctx.ReadValue<Vector2>();
        activeItems.TryGetValue(CurrentSelected, out GameObject result);
        _hotBar[CurrentSelected].color = Color.white;
        if (activeItems.ContainsKey(CurrentSelected) && result != null)
        {
            activeItems[CurrentSelected].SetActive(false);
            _hotBar[CurrentSelected].color = Color.blue;
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
            pickUpMechanic.SetCarryItem(false);
        }
        _hotBar[CurrentSelected].color = Color.red;

        if (activeItems[CurrentSelected] != null)
        {
            pickUpMechanic.SetCarryItem(true); 
        }
        else
        {
            pickUpMechanic.SetCarryItem(false);
        }
        pickUpMechanic.SetCurrentSelected(activeItems[CurrentSelected]);
        
        Debug.Log(activeItems[CurrentSelected]);
    }
    private List<Item> FindInventoryItems()
    {
        IEnumerable<Item> inventoryList = FindObjectsByType<Item>(FindObjectsSortMode.None);
        return new List<Item>(inventoryList);
    }



}
