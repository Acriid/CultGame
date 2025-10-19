using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLock : MonoBehaviour
{
    [SerializeField] private List<GameObject> puzzleObjects = new List<GameObject>();
    private Dictionary<int,GameObject> curPuzzleObjects = new Dictionary<int,GameObject>();
    private int dictionarySize = 0;
    public void UnlockAction()
    {
        //Todo- Something unlocks
        Debug.Log("ItemLock Undone");
    }
    public void SolvingAction(GameObject solveObject)
    {
        foreach (GameObject puzzleObject in puzzleObjects)
        {
            if (puzzleObject == solveObject)
            {
                dictionarySize++;
                curPuzzleObjects.Add(dictionarySize, solveObject);
            }
        }
        if (dictionarySize == puzzleObjects.Count)
        {
            UnlockAction();
        }
        solveObject.transform.SetParent(gameObject.transform);
    }
    public void SolvingAction(int solveNumber)
    {   
    }
}
