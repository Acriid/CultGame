using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    [Header("Transforms")]
    public Transform playerCamera;
    public Transform mirrorCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPlayer = mirrorCamera.InverseTransformPoint(playerCamera.position);
        transform.position = mirrorCamera.TransformPoint(new Vector3(localPlayer.x, localPlayer.y, -localPlayer.z));

        Vector3 lookatmirror = mirrorCamera.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        transform.LookAt(lookatmirror);
    }
}
