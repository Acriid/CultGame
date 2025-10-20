using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LoadScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainMenu;

    public void LoadSceneFunction(int scenceID)
    {
        StartCoroutine(LoadSceneAsync(scenceID));
    }
    
    IEnumerator LoadSceneAsync(int scenceID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenceID);
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {

            yield return null;
        }
    }
}
