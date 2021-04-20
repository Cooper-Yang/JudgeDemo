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
        DeskTop.Back.gameObject.SetActive(false);
        DeskTop.BoardHitbox.GetComponent<SpriteRenderer>().sortingOrder = -50;
        DeskTop.StartCoroutine(DeskTop.LerpCamPos(DeskTop.DeskTopCamPos, DeskTop.DeskCamSize));

        DeskTop.MonitorHitbox.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        DeskTop.BoardHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        DeskTop.NotebookHitbox.gameObject.GetComponent<PolygonCollider2D>().enabled = true;

        DeskTop.NoteBook.gameObject.SetActive(false);
        DeskTop.ImageAfterLerp = false;
        SoundMan.me.AmbienceZoomOut();
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
