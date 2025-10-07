using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using System;

public class CodeLockEvents : MenuEvents, iLocks
{
    [SerializeField] private int unlockCode;
    private int codeLengthLimit = 5;
    [SerializeField] private TMP_Text codeText;
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

    public void UnlockAction()
    {
        //Todo - Add opening animation/ play sound
        //Temporary code will just make lock disappear
        gameObject.transform.parent.gameObject.SetActive(false);
        Debug.Log("Unlocked");
    }
    public void SolvingAction(GameObject solveObject)
    {
    }
    public void SolvingAction(int solveNumber)
    {
        if (solveNumber == unlockCode) UnlockAction();
        else return;
    }
    public void ShowCurrentCode(int addedNumber)
    {
        if (addedNumber == 10)
        {
            SolvingAction(int.Parse(codeText.text));
            return;
        }
        else if (addedNumber == -1)
        {
            codeText.text = codeText.text.Substring(0, codeText.text.Length - 1);
            return;
        }
        if (codeText.text.Length == codeLengthLimit)
        {
            //ToDo - Call a "Full" function
            return;
        }
        codeText.text = codeText.text + addedNumber.ToString();

    }
}
