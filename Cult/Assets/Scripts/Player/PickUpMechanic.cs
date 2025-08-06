using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpMechanic : MonoBehaviour
{
    [Header("Oriantation Transform")]
    [SerializeField] public Transform oriantation;
    [Header("LayerMasks")]
    [SerializeField] private LayerMask itemMask;
    private InputAction interactAction;
    private RaycastHit hit;
    private bool hitItem;
    public float CheckLength = 10f;
    void Awake()
    {
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
        hitItem = Physics.Raycast(oriantation.position, transform.forward, out hit, CheckLength, itemMask);
    }
}
