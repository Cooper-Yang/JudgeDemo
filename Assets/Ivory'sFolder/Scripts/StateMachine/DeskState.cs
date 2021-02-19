using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskState : StateBase
{

    public override void EnterState(DeskTopCtrl DeskTop)
    {
        //Zoom out to desk top
        //Disable Computer Stuf
        //Enable DeskTop stuff
        DeskTop.StartCoroutine(DeskTop.LerpCamPos(DeskTop.DeskTopCamPos));
        DeskTop.Monitor.gameObject.SetActive(true);
    }

    public override void StayOnState(DeskTopCtrl DeskTop)
    {
        //interaction with objects
        //run background animation
    }

    public override void LeaveState(DeskTopCtrl DeskTop)
    {

    }

}
