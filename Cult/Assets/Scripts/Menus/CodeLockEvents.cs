using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using System;

public class CodeLockEvents : MenuEvents
{
    [SerializeField] private int unlockCode;
    public override void Awake()
    {
        base.Awake();
        if (unlockCode.ToString().Length != Selectables.Count)
        {
            Debug.LogError("Unlock Code not equal to selectables count");
        }
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
        int newnumber = int.Parse(EventSystem.current.currentSelectedGameObject.
        GetComponentInChildren<TMP_Text>().text) + (int)ctx.ReadValue<Vector2>().y;

        if (newnumber > 9) newnumber = 0;
        else if (newnumber < 0) newnumber = 9;

        EventSystem.current.currentSelectedGameObject.
        GetComponentInChildren<TMP_Text>().text = newnumber.ToString();


        string sendNumberstring = Selectables[0].GetComponentInChildren<TMP_Text>().text;
        int sendNumber;
        for (int i = 1; i < Selectables.Count; i++)
        {
            if (Selectables[i].GetComponentInChildren<TMP_Text>() != null)
            {
                sendNumberstring = string.Concat(sendNumberstring, (string)Selectables[i].GetComponentInChildren<TMP_Text>().text);
            }

        }
        sendNumber = int.Parse(sendNumberstring);
        SolvingAction(sendNumber);

    }


    public void UnlockAction()
    {
        //Todo - Add opening animation/ play sound
        //Temporary code will just make lock disappear
        //gameObject.SetActive(false);
        Debug.Log("IT UNLOCKED WOOOOOW");
    }
    public void SolvingAction(GameObject solveObject)
    {
    }
    public void SolvingAction(int solveNumber)
    {
        if (solveNumber == unlockCode) UnlockAction();
    }
}
