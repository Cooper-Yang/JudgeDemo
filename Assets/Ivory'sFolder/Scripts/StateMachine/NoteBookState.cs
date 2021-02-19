using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookState : StateBase
{
    public override void EnterState(DeskTopCtrl DeskTop)
    {
        //Zoom in to note book
        //Enable Notebook Script

    }

    public override void StayOnState(DeskTopCtrl DeskTop)
    {

    }

    public override void LeaveState(DeskTopCtrl DeskTop)
    {
        //Disable Notebook Script
    }
}
