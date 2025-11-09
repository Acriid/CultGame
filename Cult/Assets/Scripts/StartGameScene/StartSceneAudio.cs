using System.Collections;
using UnityEngine;

public class StartSceneAudio : MonoBehaviour
{
    public AudioClip[] NarratorAudios;
    public AudioClip[] DetectiveAudios;
    public AudioClip CultistAudios;
    public Transform playerObject;
    void OnEnable()
    {
        StartCoroutine(PlayAudio1());
    }
    IEnumerator PlayAudio1()
    {
        SoundManager.instance.PlaySoundClip(NarratorAudios[0], playerObject, 1f);
        yield return new WaitForSecondsRealtime(40f);
        StartCoroutine(PlayAudio2());
    }
    IEnumerator PlayAudio2()
    {
        SoundManager.instance.PlaySoundClip(DetectiveAudios[0], playerObject, 1f);
        yield return new WaitForSecondsRealtime(4f);
        SoundManager.instance.PlaySoundClip(DetectiveAudios[1], playerObject, 1f);
        yield return new WaitForSecondsRealtime(3f);
    }
}
