using UnityEngine;

public class ShowHidden : MonoBehaviour
{
    [SerializeField] private GameObject camerahidden;

    void OnEnable()
    {
        camerahidden.SetActive(true);
    }
    void OnDisable()
    {
        camerahidden.SetActive(false);
    }
}
