using UnityEngine;
using TMPro;

public class ChangeMirrorText : MonoBehaviour
{
    public TMP_Text mirrorText;
    public ItemSO itemSO;
    void OnEnable()
    {
        itemSO.OnIsInInventoryChange += ChangeText;
    }
    void OnDisable()
    {
        itemSO.OnIsInInventoryChange -= ChangeText;
    }
    private void ChangeText(bool newValue)
    {
        if (newValue) { mirrorText.text = ":)"; }
        else{mirrorText.text = "Pick up the book."; }
    }
}
