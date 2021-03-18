﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerState : StateBase
{
    public override void EnterState(DeskTopCtrl DeskTop)
    {
        //Zoom in to Computer
        //Enable Computer Script?
        //Disable Desktop Script

        DeskTop.StartCoroutine(DeskTop.LerpCamPos(DeskTop.ComputerCamPos, DeskTop.CompCamSize));
        DeskTop.MonitorHitbox.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        DeskTop.BoardHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        DeskTop.NotebookHitbox.gameObject.GetComponent<PolygonCollider2D>().enabled = false;

    }

    public override void StayOnState(DeskTopCtrl DeskTop)
    {
        if (DeskTop.ImageAfterLerp == true)
        {
            DeskTop.Back.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            DeskTop.ChangeState(DeskTop.DeskState);
        }
    }

    public override void LeaveState(DeskTopCtrl DeskTop)
    {
        
    }
}
