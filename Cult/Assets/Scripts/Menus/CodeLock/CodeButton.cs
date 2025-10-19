using UnityEngine;

public class CodeButton : MonoBehaviour
{
    //-1 for backspace 10 for confirm
    [Header("Value")]
    public int buttonValue = 0;
    public CodeLockEvents codeLockEvents;

    public void OnClick()
    {
        codeLockEvents.ShowCurrentCode(buttonValue);
    }
}
