using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        StartCoroutine(LoadScene1(Sentence[0],11f));
    }
    IEnumerator LoadScene1(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[0].text = tmptext[0].text + loadSentence[i];
            i++;
        }
        canvases[0].SetActive(false);
        canvases[1].SetActive(true);
        StartCoroutine(LoadScene2(Sentence[1],12f));
    }
    IEnumerator LoadScene2(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[1].text = tmptext[1].text + loadSentence[i];
            i++;
        }
        canvases[1].SetActive(false);
        canvases[2].SetActive(true);
        StartCoroutine(LoadScene3(Sentence[2],13f));
    }
    IEnumerator LoadScene3(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[2].text = tmptext[2].text + loadSentence[i];
            i++;
        }
        canvases[2].SetActive(false);
        canvases[3].SetActive(true);
        StartCoroutine(LoadScene4(Sentence[3],3f));
    }
    IEnumerator LoadScene4(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[3].text = tmptext[3].text + loadSentence[i];
            i++;
        }
        canvases[3].SetActive(false);
        canvases[4].SetActive(true);
        StartCoroutine(LoadScene5(Sentence[4],7f));
    }
    IEnumerator LoadScene5(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[4].text = tmptext[4].text + loadSentence[i];
            i++;
        }
        canvases[4].SetActive(false);
        canvases[5].SetActive(true);
        StartCoroutine(LoadScene6(Sentence[5],5f));
    }
    IEnumerator LoadScene6(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[5].text = tmptext[5].text + loadSentence[i];
            i++;
        }
        canvases[5].SetActive(false);
        canvases[6].SetActive(true);
        StartCoroutine(LoadScene7(Sentence[6],8f));
    }
    IEnumerator LoadScene7(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[6].text = tmptext[6].text + loadSentence[i];
            i++;
        }
        canvases[6].SetActive(false);
        canvases[7].SetActive(true);
        StartCoroutine(LoadScene8(Sentence[7],25f));
    }
    IEnumerator LoadScene8(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[7].text = tmptext[7].text + loadSentence[i];
            i++;
        }
        canvases[7].SetActive(false);
        canvases[8].SetActive(true);
        StartCoroutine(LoadScene9(Sentence[8],13f));
    }
    IEnumerator LoadScene9(string loadSentence,float WaitTime)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(WaitTime / loadSentence.Length);
            tmptext[8].text = tmptext[8].text + loadSentence[i];
            i++;
        };
        yield return new WaitForSecondsRealtime(4f);
        ShowButton();
    }

    private void ShowButton()
    {
        button.SetActive(true);
        StartCoroutine(ButtonFailsafe());
    }
    IEnumerator ButtonFailsafe()
    {
        yield return new WaitForSecondsRealtime(20f);
        EventSystem.current.SetSelectedGameObject(button);
        EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Button>().onClick.Invoke();
    }
}
