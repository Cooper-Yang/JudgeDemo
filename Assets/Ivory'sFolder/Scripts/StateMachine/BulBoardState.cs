using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulBoardState : StateBase
{

    public override void EnterState(DeskTopCtrl DeskTop)
    {
        DeskTop.StartCoroutine(DeskTop.LerpCamPos(DeskTop.BulBoardCamPos, DeskTop.BulBoardCamSize));
        DeskTop.BoardHitbox.gameObject.SetActive(false);
        DeskTop.MonitorHitbox.gameObject.SetActive(false);
        DeskTop.NotebookHitbox.gameObject.SetActive(false);
    }

    public override void StayOnState(DeskTopCtrl DeskTop)
    {
        if(DeskTop.ImageAfterLerp == true)
        {
            DeskTop.BulBoard.SetActive(true);
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
