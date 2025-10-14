using System.Collections;
using UnityEngine;

public class DoorInteraction : Interactable
{
    private bool DoorOpen = false;
    public Animator doorAnimation;
    public override void Interact()
    {
        if (DoorOpen) { CloseDoor(); }
        else{ OpenDoor(); }
    }

    private void OpenDoor()
    {
        doorAnimation.ResetTrigger("Close");
        doorAnimation.SetTrigger("Open");
        DoorOpen = true;
    }
    private void CloseDoor()
    {
        doorAnimation.ResetTrigger("Open");
        doorAnimation.SetTrigger("Close");
        DoorOpen = false;
    }
}
