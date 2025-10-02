using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryEvents : MenuEvents
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void OnEnable()
    {
        base.OnEnable();


    }
    public override void OnDisable()
    {
        base.OnDisable();
    }
    protected override IEnumerator SelectAfterDelay()
    {
        return base.SelectAfterDelay();
    }
    protected override void AddSelectionListners(Selectable selectable)
    {
        base.AddSelectionListners(selectable);
        EventTrigger trigger = selectable.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = selectable.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry BeginDrag = new EventTrigger.Entry
        {
            eventID = EventTriggerType.BeginDrag
        };

        BeginDrag.callback.AddListener(OnBeginDrag);
        trigger.triggers.Add(BeginDrag);

        EventTrigger.Entry EndDrag = new EventTrigger.Entry
        {
            eventID = EventTriggerType.EndDrag
        };

        EndDrag.callback.AddListener(OnEndDrag);
        trigger.triggers.Add(EndDrag);



    }
    protected override void OnButtonPress(InputAction.CallbackContext ctx)
    {
        base.OnButtonPress(ctx);
    }
    protected override void OnNavigate(InputAction.CallbackContext ctx)
    {
        base.OnNavigate(ctx);
    }
    private void OnBeginDrag(BaseEventData eventData)
    {

    }
    private void OnEndDrag(BaseEventData eventData)
    {

    }
}
