using UnityEngine;

public class CodeRitual : RitualBase
{
    public GameObject codeCanvas;
    public AudioClip PuzzleCorrect;
    public AudioClip PuzzleIncorrect;
    public Transform playerTransform;
    public override void CheckRitualProgress()
    {
        bool RitualDone = true;
        bool allHaveObject = true;
        foreach (GameObject RitualObject in ritualObjects)
        {
            if (!RitualObject.GetComponent<RitualObjects>().CorrectObject)
            {
                RitualDone = false;
            }
            if (!RitualObject.GetComponent<RitualObjects>().HasObject)
            {
                allHaveObject = false;
            }
        }

        if(allHaveObject && !RitualDone)
        {
            SoundManager.instance.PlaySoundClip(PuzzleIncorrect, playerTransform, 1f);
        }
        if (RitualDone)
        {
            SoundManager.instance.PlaySoundClip(PuzzleCorrect, playerTransform, 1f);
            FinishRitual();
            return;
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
