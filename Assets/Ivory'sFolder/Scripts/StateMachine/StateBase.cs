using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  StateBase
{
    public abstract void EnterState(DeskTopCtrl DeskTop);
    public abstract void StayOnState(DeskTopCtrl DeskTop);
    public abstract void LeaveState(DeskTopCtrl DeskTop);
    
}
