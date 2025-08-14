using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Properties")]
    [SerializeField] public string ItemDescription;
    [SerializeField] public Sprite ItemSprite;
    [SerializeField] public bool IsInInventory;
}

public enum ItemType
{
    Key,
    PuzzlePart,
    Ammo,
    Hint,
    Obstacle

}
