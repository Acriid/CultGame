//How to DRAW inside Unity! (Paint, Design, Strategize, Mod | Unity Tutorial)
//Code Monkey
//2025/08/21
//version 1
//https://www.youtube.com/watch?app=desktop&v=XozHdfHrb1U
using UnityEngine;
using UnityEngine.InputSystem;

public class Draw : MonoBehaviour
{
    [SerializeField] private float MouseChangeDistance = 0.1f;
    private Mesh mesh;
    public new Camera camera;
    public LayerMask uiLayerMask;
    private InputAction MouseInput;
    private Vector3 lastMousePosition = Vector3.zero;
    private MeshCollider meshCollider;
    private bool MouseDown = false;
    public float LineThickness = 0.05f;
    public bool onFloor;
    public bool onZWall;
    public bool onXWall;
    void Awake()
    {
        InitializeMouseInput();
        if (onFloor) { this.transform.localPosition = new Vector3(0f, 0.001f, 0f); }
        else if (onZWall) { this.transform.localPosition = new Vector3(0f, 0f, -0.001f); }
        else if (onXWall) { this.transform.localPosition = new Vector3(-0.001f, 0f, 0f); }
    }
    void OnEnable()
    {
        Awake();
    }
    void OnDisable()
    {
        CleanUpMouseInput();
    }
    void OnDestroy()
    {
        OnDisable();
    }
    void Update()
    {

        if (MouseDown)
        {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            bool drawHit = Physics.Raycast(ray, out RaycastHit hit, 100f, uiLayerMask);
            if (drawHit && Vector3.Distance(hit.point, lastMousePosition) > MouseChangeDistance)
            {
                Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
                Vector2[] uv = new Vector2[mesh.uv.Length + 2];
                int[] triangles = new int[mesh.triangles.Length + 6];

                mesh.vertices.CopyTo(vertices, 0);
                mesh.uv.CopyTo(uv, 0);
                mesh.triangles.CopyTo(triangles, 0);

                int vIndex = vertices.Length - 4;
                int vIndex0 = vIndex + 0;
                int vIndex1 = vIndex + 1;
                int vIndex2 = vIndex + 2;
                int vIndex3 = vIndex + 3;
                Vector3 normal = Vector3.zero;
                Vector3 mouseForwardVector = (hit.point - lastMousePosition).normalized;
                if (onFloor) { normal = new Vector3(0f, 1f, 0f); }
                if(onZWall) { normal = new Vector3(0f, 0f, -1f); }
                if (onXWall) { normal = new Vector3(-1f, 0f, 0f); }
                Vector3 newVertexUp = hit.point + Vector3.Cross(mouseForwardVector, normal) * LineThickness;
                Vector3 newVertexDown = hit.point + Vector3.Cross(mouseForwardVector, -normal) * LineThickness;

                vertices[vIndex2] = newVertexUp;
                vertices[vIndex3] = newVertexDown;

                uv[vIndex2] = Vector2.zero;
                uv[vIndex3] = Vector2.zero;

                int tIndex = triangles.Length - 6;

                triangles[tIndex + 0] = vIndex0;
                triangles[tIndex + 1] = vIndex2;
                triangles[tIndex + 2] = vIndex1;

                triangles[tIndex + 3] = vIndex1;
                triangles[tIndex + 4] = vIndex2;
                triangles[tIndex + 5] = vIndex3;

                mesh.vertices = vertices;
                mesh.uv = uv;
                mesh.triangles = triangles;

                lastMousePosition = hit.point;
            }

        }
    }

    void InitializeMouseInput()
    {
        if (MouseInput == null)
        {
            MouseInput = InputManager.instance.inputActions.Player.Attack;
            MouseInput.performed += MouseHold;
            MouseInput.canceled += MouseUp;
        }
    }
    void CleanUpMouseInput()
    {
        if (MouseInput != null)
        {
            MouseInput.performed -= MouseHold;
            MouseInput.canceled -= MouseUp;
            MouseInput = null;
        }
    }
    void MouseHold(InputAction.CallbackContext ctx)
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        bool drawHit = Physics.Raycast(ray, out RaycastHit hit, 100f, uiLayerMask);
        if (!MouseDown && drawHit)
        {
            MouseDown = true;
            mesh = new Mesh();

            lastMousePosition = hit.point;

            Vector3[] vertices = new Vector3[4];
            Vector2[] uv = new Vector2[4];
            int[] triangles = new int[6];

            vertices[0] = lastMousePosition;
            vertices[1] = lastMousePosition;
            vertices[2] = lastMousePosition;
            vertices[3] = lastMousePosition;

            uv[0] = Vector2.zero;
            uv[1] = Vector2.zero;
            uv[2] = Vector2.zero;
            uv[3] = Vector2.zero;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 1;

            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.MarkDynamic();

            GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
    void MouseUp(InputAction.CallbackContext ctx)
    {
        if (MouseDown)
        {
            MouseDown = false;
        }
    }
}
