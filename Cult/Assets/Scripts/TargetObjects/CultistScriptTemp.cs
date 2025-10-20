using UnityEngine;

public class CultistScriptTemp : MonoBehaviour
{
    public GameObject canvas;
    void Start()
    {
        if (canvas.activeSelf) canvas.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}
