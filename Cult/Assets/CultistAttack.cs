using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CultistAttack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            KillPlayer();
        }
    }
    void KillPlayer()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
