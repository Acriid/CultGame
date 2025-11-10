using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishGame : MonoBehaviour
{
    public GameObject[] Canvas;
    public AudioClip[] EndAudio;
    public Transform playerTransform;
    void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Canvas[0].SetActive(true);
        InputManager.instance.StopInput();
        StartCoroutine(EndGame());
    }
    IEnumerator EndGame()
    {
        SoundManager.instance.PlayRandomSoundClip(EndAudio, playerTransform, 1f);
        yield return new WaitForSecondsRealtime(30f);
        Canvas[0].SetActive(false);
        Canvas[1].SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        Canvas[1].SetActive(false);
        Canvas[2].SetActive(true);
        yield return new WaitForSecondsRealtime(10f);
        InputManager.instance.StartInput();
        SceneManager.LoadScene(0);
    }
}
