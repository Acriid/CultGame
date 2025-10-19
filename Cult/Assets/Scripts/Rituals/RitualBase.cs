using System.Collections.Generic;
using UnityEngine;

public class RitualBase : MonoBehaviour
{
    public List<GameObject> ritualObjects = new List<GameObject>();
    public virtual void CheckRitualProgress()
    {
        
    }

    public virtual void FinishRitual()
    {
        Debug.Log("Ritual DOne");
    }
}
