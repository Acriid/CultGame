using UnityEngine;

public class ShowHidden : MonoBehaviour
{
    [SerializeField] private GameObject camerahidden;

    void OnEnable()
    {
        if (camerahidden == null) return;
        camerahidden.SetActive(true);
    }
    void OnDisable()
    {
        if (camerahidden == null) return;
        camerahidden.SetActive(false);
    }
}
