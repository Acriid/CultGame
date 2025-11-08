using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InteractMechanic : MonoBehaviour
{
    public static InteractMechanic instance;
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
    [SerializeField] private Material outLineMaterial;
    public GameObject CurrentSelectedItem;
    private bool hitInteractable;
    private bool hitSurface;
    private bool carryItem = false;
    public float CheckLength = 10f;
    public GameObject popupCanvas;
    public Player player;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one InterActMechanic instance");
        }
        instance = this;
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
            if(CurrentSelectedItem.GetComponent<Item>().itemSO.PritoryItem){ return; }
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
            else if (CurrentSelectedItem.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.Interact();
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
        //player.animator.SetTrigger("PickUp");
        Rigidbody itenRigidbody = itemToPickUp.GetComponent<Rigidbody>();
        if (itenRigidbody != null)
        {
            itenRigidbody.useGravity = false;
            itenRigidbody.linearVelocity = Vector3.zero;
            itenRigidbody.freezeRotation = true;
        }

        //Item characteristic changes
        itemToPickUp.GetComponent<Collider>().excludeLayers = LayerMask.NameToLayer("Everything");
        itemToPickUp.transform.localScale = itemToPickUp.transform.localScale / 2;
        itemToPickUp.transform.SetParent(this.transform);
        itemToPickUp.transform.localPosition = Vector3.zero;
        itemToPickUp.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        itemToPickUp.layer = LayerMask.NameToLayer("Equipped");
        carryItem = true;


        itemToPickUp.GetComponent<Item>().ActivateScript();
        itemToPickUp.GetComponent<Item>().itemSO.IsEquiped = true;
        if (itemToPickUp.name == "Gun")
        {
            itemToPickUp.transform.localRotation = Quaternion.Euler(-90f, 180f, 0f);
        }
    }
    public void PutDownItem(GameObject itemToPutDown)
    {
        Rigidbody itenRigidbody = itemToPutDown.GetComponent<Rigidbody>();
        if (itenRigidbody != null)
        {
            itenRigidbody.useGravity = true;
            itenRigidbody.freezeRotation = false;
        }
        //Item characteristic changes
        itemToPutDown.GetComponent<Collider>().excludeLayers = LayerMask.GetMask("Nothing");
        itemToPutDown.transform.localScale = itemToPutDown.transform.localScale * 2;
        itemToPutDown.transform.SetParent(pickUpsGameObject.transform);
        itemToPutDown.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        itemToPutDown.transform.position = surfaceHit.point + Vector3.up * 0.1f;
        itemToPutDown.layer = LayerMask.NameToLayer("PickUp");
        carryItem = false;


        itemToPutDown.GetComponent<Item>().DeActivateScript();
        itemToPutDown.GetComponent<Item>().itemSO.IsEquiped = false;
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
