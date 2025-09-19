using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractMechanic : MonoBehaviour
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
    private GameObject CurrentSelectedItem;
    private bool hitInteractable;
    private bool hitSurface;
    private bool carryItem = false;
    public float CheckLength = 10f;
    public GameObject popupCanvas;
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
            PutDownItem(CurrentSelectedItem);
            InventoryManager.instance.RemoveFromInventory();
            CurrentSelectedItem = null;
        }
        else if (hitInteractable)
        {
            CurrentSelectedItem = itemHit.collider.gameObject;
            if (CurrentSelectedItem.CompareTag("PickUp"))
            {
                if (!InventoryManager.instance.InventoryFull())
                {
                    PickUpItem(CurrentSelectedItem);
                    InventoryManager.instance.AddtoInventory(CurrentSelectedItem.GetComponent<Item>());
                }
            }
            else if (CurrentSelectedItem.GetComponent<Interactable>() != null)
            {
                Interactable interactable = CurrentSelectedItem.GetComponent<Interactable>();
                if (MenuManager.instance.currentMenu != MenuManager.MenuType.None)
                {
                    interactable.HideCanvas();
                }
                else
                {
                    interactable.ShowCanvas();
                }

            }

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
        hitSurface = Physics.Raycast(ray, out surfaceHit, CheckLength, surfaceMask);
        hitInteractable = Physics.Raycast(ray, out itemHit, CheckLength, itemMask, QueryTriggerInteraction.UseGlobal);
        
        Debug.DrawRay(ray.origin, ray.direction * CheckLength, Color.red, 0.2f);
        if (hitInteractable)
        {
            popupCanvas.SetActive(true);
        }
        else
        {
            popupCanvas.SetActive(false);
        }
    }
    public void PickUpItem(GameObject itemToPickUp)
    {
        Rigidbody itenRigidbody = itemToPickUp.GetComponent<Rigidbody>();
        itenRigidbody.useGravity = false;
        itenRigidbody.linearVelocity = Vector3.zero;
        itenRigidbody.freezeRotation = true;
        itemToPickUp.GetComponent<BoxCollider>().excludeLayers = LayerMask.NameToLayer("Everything");
        itemToPickUp.transform.SetParent(this.transform);
        itemToPickUp.transform.localPosition = Vector3.zero;
        itemToPickUp.layer = LayerMask.NameToLayer("Equipped");
        carryItem = true;

        //Gun Exception
        if (itemToPickUp.name == "Gun")
        {
            itemToPickUp.GetComponent<ShootingMechanic>().enabled = true;
        }
    }
    public void PutDownItem(GameObject itemToPutDown)
    {
        Rigidbody itenRigidbody = itemToPutDown.GetComponent<Rigidbody>();
        itenRigidbody.useGravity = true;
        itenRigidbody.freezeRotation = false;
        itemToPutDown.GetComponent<BoxCollider>().excludeLayers = LayerMask.GetMask("Nothing");
        itemToPutDown.transform.SetParent(pickUpsGameObject.transform);
        itemToPutDown.transform.position = surfaceHit.point + new Vector3(0f, itemToPutDown.transform.localScale.y / 2f, 0f);
        itemToPutDown.layer = LayerMask.NameToLayer("PickUp");
        carryItem = false;

        //Gun Exception
        if (itemToPutDown.name == "Gun")
        {
            itemToPutDown.GetComponent<ShootingMechanic>().enabled = false;
        }
    }
    public void SetCurrentSelected(GameObject currentselected)
    {
        CurrentSelectedItem = currentselected;
    }
    public void SetCarryItem(bool newValue)
    {
        carryItem = newValue;
    }
}
