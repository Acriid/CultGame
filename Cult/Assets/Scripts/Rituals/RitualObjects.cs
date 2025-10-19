using UnityEngine;

public class RitualObjects : Interactable
{
    public GameObject ritualObject;
    public bool CorrectObject;
    private bool HasObject = false;
    public GameObject ritualObjectHolder;
    private GameObject HeldObject;
    public override void Interact()
    {
        if(HasObject)
        {
            Debug.Log("WOW");
            InventoryManager.instance.AddtoInventory(HeldObject.GetComponent<Item>());
            InteractMechanic.instance.PickUpItem(HeldObject);
            HasObject = false;
            return;
        }
        else if (InventoryManager.instance.getCurrentHeldObject() != null)
        {

            GameObject TempObject = InventoryManager.instance.getCurrentHeldObject();
            InventoryManager.instance.RemoveFromInventory();
            if (TempObject.GetComponent<Item>().itemSO.PritoryItem) return;
            InteractMechanic.instance.PutDownItem(TempObject);
            TempObject.transform.SetParent(ritualObjectHolder.transform);
            TempObject.transform.localPosition = Vector3.zero;
            HeldObject = TempObject;
            HasObject = true;
        }  
        
    }
}
