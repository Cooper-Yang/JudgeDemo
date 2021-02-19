using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTopCtrl : MonoBehaviour
{
    //variables needed: 
    //Camera(positions)
    //Computer Code -- GameObject CONTROL
    //Notebook Code
    //Computer Sprite
    //Notebook Sprite
    //Mouse Pos

    public Camera mainCamera;
    public Vector3 DeskTopCamPos = new Vector3(0, 0, -10);
    public Vector3 ComputerCamPos = new Vector3(0, 0.6f, -3.6f);
    public Vector3 NotebookCamPos;

    public GameObject Monitor;


    private StateBase currentState;
    public StateBase DeskState = new DeskState();
    public StateBase ComputerState = new ComputerState();


    //have different state here
    //be able to change state

    public void ChangeState(StateBase NewState)
    {
        if (currentState != null)
        {
            currentState.LeaveState(this);
        }

        currentState = NewState;

        if (currentState != null)
        {
            currentState.EnterState(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        ChangeState(DeskState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.StayOnState(this);
    }

    public IEnumerator LerpCamPos(Vector3 LerpGoal)
    {
        float lerpTime = 0.3f;
        float timer = 0;
        Vector3 currentCamPos = mainCamera.transform.position;
        while (timer < lerpTime)
        {
            Vector3 LerpValue = Vector3.Lerp(currentCamPos, LerpGoal, timer / lerpTime);
            mainCamera.transform.position = LerpValue;
            timer += Time.deltaTime;
            if(Monitor.GetComponent<HighLight>().enabled == true)
            {
                Monitor.GetComponent<HighLight>().enabled = false;
            }
            yield return null;
        }
        mainCamera.transform.position = LerpGoal;
    }

}
