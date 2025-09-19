using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CustomInputSystem inputActions;
    public static InputManager instance { get; private set; }
    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Debug.LogWarning("More than one instance of InputManager.");
            }
        }
        instance = this;
        InitializeInput();
    }
    void OnEnable()
    {
        Awake();
    }
    void OnDisable()
    {
        CleanUpInput();
    }

    void OnDestroy()
    {
        CleanUpInput();
    }

    void InitializeInput()
    {
        if (inputActions == null)
        {
            inputActions = new CustomInputSystem();
            inputActions.Enable();
        }
    }
    void CleanUpInput()
    {
        if (inputActions != null)
        {
            inputActions.Dispose();
            inputActions.Disable();
            inputActions = null;
        }
    }
}
