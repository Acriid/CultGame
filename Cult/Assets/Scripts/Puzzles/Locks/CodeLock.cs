using UnityEngine;

public class CodeLock : MonoBehaviour
{
    [SerializeField] private int unlockCode;
    public void UnlockAction()
    {
        //Todo - Add opening animation/ play sound
        //Temporary code will just make lock disappear
        gameObject.SetActive(false);
    }
    public void SolvingAction(GameObject solveObject)
    {
    }
    public void SolvingAction(int solveNumber)
    {
        if (solveNumber == unlockCode) UnlockAction();
    }
}
