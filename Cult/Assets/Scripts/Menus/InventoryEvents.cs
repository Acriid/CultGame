using System.Collections;
using UnityEngine;
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
    }
    protected override void OnButtonPress(InputAction.CallbackContext ctx)
    {
        base.OnButtonPress(ctx);
    }
    protected override void OnNavigate(InputAction.CallbackContext ctx)
    {
        base.OnNavigate(ctx);
    }
}
