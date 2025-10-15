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
        doorAnimation.SetBool("Open", true);
        doorAnimation.SetBool("Close", false);
        DoorOpen = true;
    }
    private void CloseDoor()
    {
        doorAnimation.SetBool("Close", true);
        doorAnimation.SetBool("Open", false);
        DoorOpen = false;
    }
}
