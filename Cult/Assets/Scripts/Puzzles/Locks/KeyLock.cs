using UnityEngine;

public class KeyLock : MonoBehaviour, iLocks
{
    [SerializeField] private GameObject unlockKey;
    public void UnlockAction()
    {
        //Todo - Add opening animation/ play sound
        //Temporary code will just make lock dissapear
        gameObject.SetActive(false);
    }
    public void SolvingAction(GameObject solveObject)
    {
        if (solveObject == unlockKey)
        {
            UnlockAction();
        }
    }
    public void SolvingAction(int solveNumber)
    {
        
    }
}
