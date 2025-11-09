using UnityEngine;

public class RitualFind : MonoBehaviour
{
    public AudioClip ProtagAudio;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Played Audio");
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlaySoundClip(ProtagAudio, other.transform, 1f);
        }
        this.gameObject.SetActive(false);
    }
}
