using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookState : StateBase
{
    public override void EnterState(DeskTopCtrl DeskTop)
    {
        //Zoom in to note book
        //Enable Notebook Script
        
        DeskTop.StartCoroutine(DeskTop.LerpCamPos(DeskTop.NotebookCamPos, DeskTop.NotebookCamSize));
        DeskTop.MonitorHitbox.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        DeskTop.BoardHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        DeskTop.NotebookHitbox.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        SoundMan.me.AmbienceZoomIn();
    }

    public override void StayOnState(DeskTopCtrl DeskTop)
    {
        if (DeskTop.ImageAfterLerp == true)
        {
            DeskTop.NoteBook.SetActive(true);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            DeskTop.ChangeState(DeskTop.DeskState);
        }
    }

    public override void LeaveState(DeskTopCtrl DeskTop)
    {
        //Disable Notebook Script
    }
}
