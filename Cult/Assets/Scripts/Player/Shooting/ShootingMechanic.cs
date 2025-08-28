using System.Collections;
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
    ShootAble shootable;
    public GameObject popupCanvas;
    public LayerMask targetMask;
    void Awake()
    {
        // EnableShootAction();
    }
    void OnEnable()
    {
        EnableShootAction();
        StartCoroutine(CheckForTarget());
    }
    void OnDisable()
    {
        DisableShootAction();
        StopAllCoroutines();
        SafetyDisable();
        Debug.Log("No more gun");
    }
    void OnDestroy()
    {
        DisableShootAction();
        StopAllCoroutines();
        SafetyDisable();
        Debug.Log("No more gun");
    }
    void EnableShootAction()
    {
        if (shootAction == null)
        {
            shootAction = InputManager.instance.inputActions.Player.Attack;
            shootAction.performed += OnShootAction;
        }
    }

    void DisableShootAction()
    {
        if (shootAction != null)
        {
            shootAction.performed -= OnShootAction;
            shootAction = null;
        }
    }

    void OnShootAction(InputAction.CallbackContext ctx)
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        hit = Physics.Raycast(ray, out bulletHit, gunRange, targetMask);

        if (hit)
        {
            hitObject = bulletHit.collider.gameObject;
            Debug.Log(bulletHit.collider.gameObject);
            shootable = hitObject.GetComponent<ShootAble>();
            if (shootable != null)
            {
                shootable.gameObject.SetActive(false);
                StartCoroutine(waitForRespawn());
            }
        }
    }
    private IEnumerator CheckForTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            bool CheckHit = Physics.Raycast(ray, out bulletHit, gunRange, targetMask);
            if (CheckHit)
            {
                popupCanvas.SetActive(true);
            }
            else
            {
                popupCanvas.SetActive(false);
            }
        }
    }
    private IEnumerator waitForRespawn()
    {
        yield return new WaitForSeconds(3f);
        shootable.gameObject.SetActive(true);
    }

    private void SafetyDisable()
    {
        if (popupCanvas.activeInHierarchy)
        {
            popupCanvas.SetActive(false);
        }
        if (shootable != null && !shootable.gameObject.activeInHierarchy )
        {
            shootable.gameObject.SetActive(true);
        }
    }
}
