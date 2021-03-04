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
        DeskTop.StartCoroutine(DeskTop.LerpCamPos(DeskTop.DeskTopCamPos, DeskTop.DeskCamSize));

        DeskTop.MonitorHitbox.gameObject.SetActive(true);
        DeskTop.BoardHitbox.gameObject.SetActive(true);
        DeskTop.NotebookHitbox.gameObject.SetActive(true);

        DeskTop.BulBoard.SetActive(false);
        DeskTop.NoteBook.SetActive(false);

        DeskTop.ImageAfterLerp = false;
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
