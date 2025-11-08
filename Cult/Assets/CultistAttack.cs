using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CultistAttack : MonoBehaviour
{
    public GameObject deathScreen;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            KillPlayer();
        }
    }
    void KillPlayer()
    {
        InputManager.instance.StopInput();
        deathScreen.SetActive(true);

    }
}
