using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StartSceneThing : MonoBehaviour
{
    public string Sentence = "Wake up...";
    public TMP_Text tmptext;
    public GameObject button;
    void Start()
    {
        StartCoroutine(LoadSentence(Sentence));
        StartCoroutine(ShowButton());
    }
    IEnumerator LoadSentence(string loadSentence)
    {
        int i = 0;
        while (i < loadSentence.Length)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            tmptext.text = tmptext.text + loadSentence[i];
            i++;
        }
    }
    IEnumerator ShowButton()
    {
        yield return new WaitForSecondsRealtime(4f);
        button.SetActive(true);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
