using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class CodeLockEvents : MenuEvents
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
        int newnumber = int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text)
         + (int)ctx.ReadValue<Vector2>().y;
        EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text = newnumber.ToString();

    }
}
