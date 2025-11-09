using System.Collections;
using UnityEngine;

public class RandomCultistAudio : MonoBehaviour
{
    public AudioClip[] randomCultAudio;
    public AudioClip[] randomChaseAudio;
    public AiDetection aiDetection;
    public float AudioCooldown = 10f;
    void OnEnable()
    {
        StartCoroutine(PlayRandomAudioClip());
        StartCoroutine(PlayChaseAudio());
    }
    IEnumerator PlayRandomAudioClip()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            if (!aiDetection.canSeePlayer)
            {
                SoundManager.instance.PlayRandomSoundClip(randomCultAudio, aiDetection.gameObject.transform, 2f, 1);
            }
        }
    }
    IEnumerator PlayChaseAudio()
    {
        bool PlayedSound = false;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (aiDetection.canSeePlayer && !PlayedSound && AudioCooldown > 10f)
            {
                AudioCooldown = 0f;
                PlayedSound = true;
                SoundManager.instance.PlayRandomSoundClip(randomChaseAudio, aiDetection.gameObject.transform, 1f);
            }
            else
            {
                PlayedSound = false;
            }

        }
    }
    void Update()
    {
        AudioCooldown += Time.deltaTime;
    }
}
