using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StartSceneThing : MonoBehaviour
{
    public string[] Sentence;
    public PlayerSettingsSO playerSettingsSO;
    public TMP_Text[] tmptext;
    public GameObject button;
    public GameObject[] canvases;
    public CloseCanvas closeCanvas;
    void OnEnable()
    {
        if(playerSettingsSO.EnableScene)
        {
            closeCanvas.OnClick();
            return;
        }
        StartCoroutine(LoadScene1(Sentence[0]));
    }
    IEnumerator LoadScene1(string loadSentence)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(3f / loadSentence.Length);
            tmptext[0].text = tmptext[0].text + loadSentence[i];
            i++;
        }
        yield return new WaitForSecondsRealtime(2f);
        canvases[0].SetActive(false);
        canvases[1].SetActive(true);
        StartCoroutine(LoadScene2(Sentence[1]));
    }
    IEnumerator LoadScene2(string loadSentence)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(3f / loadSentence.Length);
            tmptext[1].text = tmptext[1].text + loadSentence[i];
            i++;
        }
        yield return new WaitForSecondsRealtime(2f);
        canvases[1].SetActive(false);
        canvases[2].SetActive(true);
        StartCoroutine(LoadScene3(Sentence[2]));
    }
    IEnumerator LoadScene3(string loadSentence)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(3f / loadSentence.Length);
            tmptext[2].text = tmptext[2].text + loadSentence[i];
            i++;
        }
        yield return new WaitForSecondsRealtime(2f);
        canvases[2].SetActive(false);
        canvases[3].SetActive(true);
        StartCoroutine(LoadScene4(Sentence[3]));
    }
    IEnumerator LoadScene4(string loadSentence)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(3f / loadSentence.Length);
            tmptext[3].text = tmptext[3].text + loadSentence[i];
            i++;
        }
        yield return new WaitForSecondsRealtime(2f);
        canvases[3].SetActive(false);
        canvases[4].SetActive(true);
        StartCoroutine(LoadScene5(Sentence[4]));
    }
    IEnumerator LoadScene5(string loadSentence)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(2f / loadSentence.Length);
            tmptext[4].text = tmptext[4].text + loadSentence[i];
            i++;
        }
        yield return new WaitForSecondsRealtime(4f);
        ShowButton();
    }
    
    private void ShowButton()
    {
        button.SetActive(true);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
