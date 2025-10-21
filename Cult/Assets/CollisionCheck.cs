using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.gameObject.CompareTag("Player"));
    }
}
