using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Properties")]
    public string ItemDescription;
    public Sprite ItemSprite;
    public event Action<bool> OnIsInInventoryChange;
    [SerializeField] private bool _isInInventory;
    public bool IsInInventory
    {
        get { return _isInInventory; }
        set
        {
            if(_isInInventory != value)
            {
                _isInInventory = value;
                OnIsInInventoryChange?.Invoke(_isInInventory);
            }
        }
    }
    public bool PritoryItem;
}

public enum ItemType
{
    Key,
    PuzzlePart,
    Ammo,
    Hint,
    Obstacle

}
