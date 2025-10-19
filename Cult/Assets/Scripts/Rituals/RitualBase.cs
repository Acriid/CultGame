using System.Collections.Generic;
using UnityEngine;

public class RitualBase : MonoBehaviour
{
    public List<GameObject> ritualObjects = new List<GameObject>();
    public virtual void CheckRitualProgress()
    {
        bool RitualDone = true;
        foreach (GameObject RitualObject in ritualObjects)
        {
            if (!RitualObject.GetComponent<RitualObjects>().CorrectObject)
            {
                RitualDone = false;
            }
        }

        if (RitualDone)
        {
            FinishRitual();
        }
    }

    public virtual void FinishRitual()
    {
        Debug.Log("Ritual DOne");
    }
}
