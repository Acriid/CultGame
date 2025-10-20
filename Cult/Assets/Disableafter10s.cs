using System.Collections;
using UnityEngine;

public class Disableafter10s : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        yield return new WaitForSecondsRealtime(10f);
        this.gameObject.SetActive(false);
    }
}
