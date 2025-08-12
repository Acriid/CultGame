using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpMechanic : MonoBehaviour
{
    [Header("Camera Transform")]
    [SerializeField] private new Camera camera;
    [Header("LayerMasks")]
    [SerializeField] private LayerMask itemMask;
    [SerializeField] private LayerMask surfaceMask;
    private InputAction interactAction;
    private RaycastHit itemHit;
    private RaycastHit surfaceHit;
    [Header("Pickup GameObject")]
    [SerializeField] private GameObject pickUpsGameObject;
    private GameObject pickUp;
    private bool hitItem;
    private bool hitSurface;
    private bool carryItem = false;
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
        if (carryItem && hitSurface)
        {
            carryItem = false;
            pickUp.GetComponent<Rigidbody>().useGravity = true;
            pickUp.GetComponent<BoxCollider>().excludeLayers = LayerMask.GetMask("Nothing");
            pickUp.transform.SetParent(pickUpsGameObject.transform);
            pickUp.transform.position = surfaceHit.point + new Vector3(0f,pickUp.transform.localScale.y /2f,0f);
            pickUp.layer = LayerMask.NameToLayer("PickUp");
        }
        else if (hitItem)
        {
            carryItem = true;
            pickUp = itemHit.collider.transform.gameObject;
            pickUp.GetComponent<Rigidbody>().useGravity = false;
            pickUp.GetComponent<BoxCollider>().excludeLayers = LayerMask.NameToLayer("Everything");
            pickUp.transform.SetParent(this.transform);
            pickUp.transform.localPosition = Vector3.zero;
            pickUp.layer = LayerMask.NameToLayer("Equipped");
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
        hitItem = Physics.Raycast(ray, out itemHit, CheckLength, itemMask);
        hitSurface = Physics.Raycast(ray, out surfaceHit, CheckLength, surfaceMask);
        Debug.DrawRay(ray.origin, ray.direction * CheckLength, Color.red,0.2f);
    }
}
