using UnityEngine;

public class RitualObjects : Interactable
{
    public GameObject ritualObject;
    public RitualBase ritualBase;
    public bool CorrectObject = false;
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
            CorrectObject = false;
            return;
        }
        else if (InventoryManager.instance.getCurrentHeldObject() != null)
        {

            GameObject TempObject = InventoryManager.instance.getCurrentHeldObject();
            if (TempObject.GetComponent<Item>().itemSO.PritoryItem) return;
            InventoryManager.instance.RemoveFromInventory();
            InteractMechanic.instance.PutDownItem(TempObject);
            TempObject.transform.SetParent(ritualObjectHolder.transform);
            TempObject.transform.localPosition = Vector3.zero;
            HeldObject = TempObject;
            HasObject = true;


            if(HeldObject == ritualObject)
            {
                CorrectObject = true;
            }
        }
        ritualBase.CheckRitualProgress();
    }
}
