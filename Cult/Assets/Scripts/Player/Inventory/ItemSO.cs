using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Properties")]
    public string ItemDescription;
    public Sprite ItemSprite;
    public bool IsInInventory;
}

public enum ItemType
{
    Key,
    PuzzlePart,
    Ammo,
    Hint,
    Obstacle

}
