using System.Collections;
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
        DeskTop.MonitorHitbox.gameObject.SetActive(false);
        DeskTop.BoardHitbox.gameObject.SetActive(false);
        DeskTop.NotebookHitbox.gameObject.SetActive(false);

    }

    public override void StayOnState(DeskTopCtrl DeskTop)
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            DeskTop.ChangeState(DeskTop.DeskState);
        }
    }

    public override void LeaveState(DeskTopCtrl DeskTop)
    {
        
    }
}
