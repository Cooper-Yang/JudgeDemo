﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulBoardState : StateBase
{

    public override void EnterState(DeskTopCtrl DeskTop)
    {
       
        DeskTop.StartCoroutine(DeskTop.LerpCamPos(DeskTop.BulBoardCamPos, DeskTop.BulBoardCamSize));
        DeskTop.MonitorHitbox.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        DeskTop.BoardHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        DeskTop.NotebookHitbox.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        SoundMan.me.AmbienceZoomIn();
        foreach (GameObject mat in GameObject.FindGameObjectsWithTag("Material"))
        {
            if (mat.GetComponent<BoxCollider2D>())
            {

                mat.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public override void StayOnState(DeskTopCtrl DeskTop)
    {
        if (DeskTop.ImageAfterLerp == true)
        {
            DeskTop.BoardHitbox.GetComponent<SpriteRenderer>().sortingOrder = 100;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            DeskTop.ChangeState(DeskTop.DeskState);
        }

    }

    public override void LeaveState(DeskTopCtrl DeskTop)
    {
        DeskTop.BoardHitbox.GetComponent<SpriteRenderer>().sortingOrder = -50;
        foreach (GameObject mat in GameObject.FindGameObjectsWithTag("Material"))
        {
            if (mat.GetComponent<BoxCollider2D>())
            {

                mat.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }


}
