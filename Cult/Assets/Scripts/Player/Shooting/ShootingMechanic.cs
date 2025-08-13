using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingMechanic : MonoBehaviour
{
    [Header("Camera Transform")]
    [SerializeField] private new Camera camera;
    private RaycastHit bulletHit;
    private bool hit;
    private InputAction shootAction;
    [Header("GunRange")]
    [SerializeField] private float gunRange = 100f;
    private GameObject hitObject;
    void Awake()
    {
        EnableShootAction();
    }
    void OnEnable()
    {
        EnableShootAction();
    }
    void OnDisable()
    {
        DisableShootAction();
    }

    void EnableShootAction()
    {
        if (shootAction == null)
        {
            shootAction = InputManager.instance.inputActions.Player.Attack;
            shootAction.performed += OnShootAction;
            shootAction.Enable();
        }
    }

    void DisableShootAction()
    {
        if (shootAction != null)
        {
            shootAction.performed -= OnShootAction;
            shootAction.Dispose();
            shootAction.Disable();
            shootAction = null;
        }
    }

    void OnShootAction(InputAction.CallbackContext ctx)
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        ShootAble shootable;
        hit = Physics.Raycast(ray, out bulletHit, gunRange);
        if (hit)
        {
            hitObject = bulletHit.collider.gameObject;
            shootable = hitObject.GetComponent<ShootAble>();
            if (shootable != null)
            {
                shootable.DestroyObject();
            }
        }
    }
    
}
