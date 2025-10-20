using UnityEngine;

public class CodeRitual : RitualBase
{
    public GameObject codeCanvas;
    public override void CheckRitualProgress()
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
        else if(codeCanvas.activeSelf)
        {
            codeCanvas.SetActive(false);
        }

    }
    public override void FinishRitual()
    {
        codeCanvas.SetActive(true);
    }
}
