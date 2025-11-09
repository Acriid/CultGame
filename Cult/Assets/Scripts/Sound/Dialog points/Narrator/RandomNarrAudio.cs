using System.Collections;
using UnityEngine;

public class RandomNarrAudio : MonoBehaviour
{
    public AudioClip[] randomWaiting;
    public Transform playerTransform;
    void OnEnable()
    {

    }
    IEnumerator ChooseRandomAudio()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(300f);
            SoundManager.instance.PlayRandomSoundClip(randomWaiting, playerTransform, 1f);
        }
    }
}
