using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpMechanic : MonoBehaviour
{
    [Header("Camera Transform")]
    [SerializeField] public Transform Camera;
    [SerializeField] private new Camera camera;
    [Header("LayerMasks")]
    [SerializeField] private LayerMask itemMask;
    private InputAction interactAction;
    private RaycastHit hit;
    private bool hitItem;
    public float CheckLength = 10f;
    void Awake()
    {
        StartCoroutine(SendRayCast());
        InitializeInteractAction();
    }
    void OnDisable()
    {
        CleanUpInterActAction();
    }
    void OnDestroy()
    {
        CleanUpInterActAction();
    }
    void InitializeInteractAction()
    {
        if (interactAction == null)
        {
            interactAction = InputManager.instance.inputActions.Player.Interact;
            interactAction.started += PickUpItem;
            interactAction.Enable();
        }
    }
    void CleanUpInterActAction()
    {
        if (interactAction != null)
        {
            interactAction.started -= PickUpItem;
            interactAction.Dispose();
            interactAction.Disable();
            interactAction = null;
        }
    }
    void PickUpItem(InputAction.CallbackContext ctx)
    {
        if (hitItem)
        {

        }
    }
    IEnumerator SendRayCast()
    {
        while (true)
        {
            ItemRayCast();
            yield return new WaitForSeconds(0.2f);
        }
    }
    void ItemRayCast()
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        hitItem = Physics.Raycast(ray, out hit, CheckLength, itemMask);
        Debug.DrawLine(ray.origin, hit.point, Color.red);
    }
}
