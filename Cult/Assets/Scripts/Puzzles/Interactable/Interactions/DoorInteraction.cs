using System.Collections;
using UnityEngine;

public class DoorInteraction : Interactable
{
    private bool DoorOpen = false;
    public override void Interact()
    {
        if (DoorOpen) { CloseDoor(); }
        else{ OpenDoor(); }
    }

    private void OpenDoor()
    {

    }
    private void CloseDoor()
    {
        
    }
}
