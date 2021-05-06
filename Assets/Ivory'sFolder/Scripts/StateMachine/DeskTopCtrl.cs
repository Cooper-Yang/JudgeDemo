using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Desktop Setting")]
    public float DeskCamSize = 235;
    public Vector3 DeskTopCamPos = new Vector3(0, -25, -10);
    
    [Header("Computer Setting")]
    public float CompCamSize = 75;
    public Vector3 ComputerCamPos = new Vector3(0, 2, -10);
    [Header("Bulletin Board Setting")]
    public float BulBoardCamSize = 120;
    public Vector3 BulBoardCamPos = new Vector3(137, 54, -10);
    [Header("Notebook Setting")]
    public float NotebookCamSize = 75;
    public Vector3 NotebookCamPos;
    public GameObject NoteBook;
    [Header("Hit Box")]
    public GameObject MonitorHitbox;
    public GameObject BoardHitbox;
    public GameObject NotebookHitbox;
    [Header("UI")]
    public Button Desk;
    public Button PC;
    public Button Board;
    public Button Note;
    public Button Quit;
    public bool ImageAfterLerp = false;
    private bool isShown = false;

    private StateBase currentState;

    public StateBase CurrentState
    {
        get => currentState;
    }

    public StateBase DeskState = new DeskState();
    public StateBase ComputerState = new ComputerState();
    public StateBase BulboardState = new BulBoardState();
    public StateBase NotebookState = new NoteBookState();



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
    
    void Start()
    {
        BoardHitbox.GetComponent<SpriteRenderer>().sortingOrder = 0;
        ChangeState(DeskState);
    }

    void Update()
    {
        currentState.StayOnState(this);
    }

    public void ReturnDeskTop()
    {
        ChangeState(DeskState);
    }

    public void ChangeToPC()
    {
        ChangeState(ComputerState);
    }
    public void ChangeToBoard()
    {
        ChangeState(BulboardState);
    }
    public void ChangeToNote()
    {
        ChangeState(NotebookState);
    }

    public void ShowIcon()
    {
        if(!isShown)
        {
            Desk.gameObject.SetActive(true);
            PC.gameObject.SetActive(true);
            Board.gameObject.SetActive(true);
            Note.gameObject.SetActive(true);
            Quit.gameObject.SetActive(true);
            isShown = true;

        }
        else if(isShown)
        {
            Desk.gameObject.SetActive(false);
            PC.gameObject.SetActive(false);
            Board.gameObject.SetActive(false);
            Note.gameObject.SetActive(false);
            Quit.gameObject.SetActive(false);
            isShown = false;
        }
    }

    public IEnumerator LerpCamPos(Vector3 LerpGoal, float LerpCamSize)
    {
        float lerpTime = 0.3f;
        float timer = 0;
        Vector3 currentCamPos = mainCamera.transform.position;
        float currentCamSize = mainCamera.orthographicSize;
        while (timer < lerpTime)
        {
            Vector3 LerpValue = Vector3.Lerp(currentCamPos, LerpGoal, timer / lerpTime);
            float CamSize = Mathf.Lerp(currentCamSize, LerpCamSize, timer / lerpTime);

            mainCamera.orthographicSize = CamSize;
            mainCamera.transform.position = LerpValue;
            timer += Time.deltaTime;
            ImageAfterLerp = false;
            yield return null;
        }
        mainCamera.transform.position = LerpGoal;
        if (currentState != DeskState)
        {
            ImageAfterLerp = true;
        }
    }

}
