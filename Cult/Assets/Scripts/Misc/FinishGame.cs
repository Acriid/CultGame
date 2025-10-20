using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    public GameObject Canvas;
    void OnTriggerEnter(Collider other)
    {
        Canvas.SetActive(true);
        StartCoroutine(EndGame());
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene(0);
    }
}
