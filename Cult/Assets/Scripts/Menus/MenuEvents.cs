//Handle UI Like a Commerical Game (with just ONE script) - Unity Tutorial
//Sasquatch B Studios
//2025/08/08
//Version 1
//https://www.youtube.com/watch?v=0EsrYNAAEEY&list=PLMflPwRw2X6o6FP6P5vr5SnJZfB81Un1d&index=36
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System.Collections;

public class MenuEvents : MonoBehaviour
{
    [Header("Selections")]
    public List<Selectable> Selectables = new List<Selectable>();
    [SerializeField] protected Selectable _firstSelected;
    protected Selectable _lastSelected;
    [Header("Controls")]
    [SerializeField] protected InputActionReference navigateAction;
    [SerializeField] protected InputActionReference selectAction;
    public virtual void Awake()
    {
        foreach (var selectable in Selectables)
        {
            AddSelectionListners(selectable);
        }


    }
    public virtual void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        navigateAction.action.performed += OnNavigate;
        selectAction.action.performed += OnButtonPress;

        Time.timeScale = 0;
        StartCoroutine(SelectAfterDelay());
    }
    public virtual void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        navigateAction.action.performed -= OnNavigate;
        selectAction.action.performed -= OnButtonPress;
        Time.timeScale = 1;
    }
    protected virtual IEnumerator SelectAfterDelay()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(_firstSelected.gameObject);
    }
    protected virtual void AddSelectionListners(Selectable selectable)
    {
        EventTrigger trigger = selectable.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = selectable.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry SelectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Select
        };

        SelectEntry.callback.AddListener(OnSelect);
        trigger.triggers.Add(SelectEntry);

        EventTrigger.Entry DeselectEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.Select
        };

        SelectEntry.callback.AddListener(OnDeselect);
        trigger.triggers.Add(DeselectEntry);

        EventTrigger.Entry PointerEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };

        PointerEnter.callback.AddListener(OnPointerEnter);
        trigger.triggers.Add(PointerEnter);

        EventTrigger.Entry PointerExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };

        PointerExit.callback.AddListener(OnPointerExit);
        trigger.triggers.Add(PointerExit);

    }

    public void OnSelect(BaseEventData eventData)
    {
        _lastSelected = eventData.selectedObject.GetComponent<Selectable>();
    }

    public void OnDeselect(BaseEventData eventData)
    {

    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null)
        {
            Selectable sel = pointerEventData.pointerEnter.GetComponentInParent<Selectable>();
            if (sel == null)
            {
                sel = pointerEventData.pointerEnter.GetComponentInChildren<Selectable>();
            }
            pointerEventData.selectedObject = sel.gameObject;
            //pointerEventData.selectedObject = pointerEventData.pointerEnter;

        }
    }
    public void OnPointerExit(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null)
        {
            pointerEventData.selectedObject = null;
        }
    }

    protected virtual void OnNavigate(InputAction.CallbackContext ctx)
    {
        if (EventSystem.current.currentSelectedGameObject == null && _lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(_lastSelected.gameObject);
        }
    }

    protected virtual void OnButtonPress(InputAction.CallbackContext ctx)
    {
        EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Button>().onClick.Invoke();
    }
}
