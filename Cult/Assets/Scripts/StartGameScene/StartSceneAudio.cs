using System.Collections;
using UnityEngine;

public class StartSceneAudio : MonoBehaviour
{
    public AudioClip[] NarratorAudios;
    public AudioClip[] DetectiveAudios;
    public AudioClip[] Carnoises;
    public AudioClip CultistAudios;
    public AudioClip[] DetectiveKO;
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
        yield return new WaitForSecondsRealtime(5f);
        StartCoroutine(PlayAudio3());
    }
    IEnumerator PlayAudio3()
    {
        SoundManager.instance.PlaySoundClip(Carnoises[0], playerObject, 1f);
        yield return new WaitForSecondsRealtime(1f);
        SoundManager.instance.PlaySoundClip(Carnoises[1], playerObject, 1f);
        yield return new WaitForSecondsRealtime(4f);
        StartCoroutine(PlayAudio4());
    }
    IEnumerator PlayAudio4()
    {
        SoundManager.instance.PlaySoundClip(NarratorAudios[1], playerObject, 1f);
        yield return new WaitForSecondsRealtime(8f);
        StartCoroutine(PlayAudio5());
    }
    IEnumerator PlayAudio5()
    {
        SoundManager.instance.PlaySoundClip(NarratorAudios[2], playerObject, 1f);
        yield return new WaitForSecondsRealtime(10f);
        StartCoroutine(PlayAudio6());
    }
    IEnumerator PlayAudio6()
    {
        SoundManager.instance.PlaySoundClip(DetectiveKO[0], playerObject, 1f);
        yield return new WaitForSecondsRealtime(3f);
        SoundManager.instance.PlaySoundClip(DetectiveKO[1], playerObject, 1f);
        yield return new WaitForSecondsRealtime(6f);
        SoundManager.instance.PlaySoundClip(CultistAudios, playerObject, 1f);
        yield return new WaitForSecondsRealtime(7f);
        StartCoroutine(PlayAudio7());
    }
    IEnumerator PlayAudio7()
    {
        SoundManager.instance.PlaySoundClip(NarratorAudios[3], playerObject, 1f); 
        yield return new WaitForSecondsRealtime(13f);       
    }
}
