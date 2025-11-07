using UnityEngine;

public class RitualObjects : Interactable
{
    public GameObject ritualObject;
    public RitualBase ritualBase;
    public GameObject Rituallight;
    public bool CorrectObject = false;
    private bool HasObject = false;
    public GameObject ritualObjectHolder;
    private GameObject HeldObject;
    public override void Interact()
    {
        if(HasObject)
        {
            InventoryManager.instance.AddtoInventory(HeldObject.GetComponent<Item>());
            InteractMechanic.instance.PickUpItem(HeldObject);
            foreach(Collider collider in HeldObject.GetComponents<Collider>())
            {
                collider.enabled = false;
            }
            HeldObject = null;
            HasObject = false;
            CorrectObject = false;
            Rituallight.SetActive(false);
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
            TempObject.GetComponent<Rigidbody>().useGravity = false;
            foreach (Collider collider in TempObject.GetComponents<Collider>())
            {
                collider.enabled = false;
            }
            TempObject.GetComponent<Rigidbody>();
            HeldObject = TempObject;
            HasObject = true;


            if(HeldObject == ritualObject)
            {
                CorrectObject = true;
            }
        }
        ritualBase.CheckRitualProgress();
        if (CorrectObject)
        {
            Rituallight.SetActive(true);
        }
        else
        {
            Rituallight.SetActive(false);
        }
    }
}
