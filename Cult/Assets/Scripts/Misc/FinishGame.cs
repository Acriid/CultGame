using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    public GameObject Canvas;
    void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Canvas.SetActive(true);
        InputManager.instance.StopInput();
        StartCoroutine(EndGame());
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(10f);
        InputManager.instance.StartInput();
        SceneManager.LoadScene(0);
    }
}
