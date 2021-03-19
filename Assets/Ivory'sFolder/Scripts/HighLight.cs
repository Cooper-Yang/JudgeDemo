using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    public DeskTopCtrl deskcon;
    private Color StartColor = Color.white;
    private Color HightlightColor = Color.yellow;

    private void Update()
    {
        if (transform.childCount > 0)
        {
            GetComponentInChildren<Canvas>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

    void OnMouseEnter()
    {
        CursorManager.Instance.ChangeToClick();
        this.GetComponent<Renderer>().material.color = HightlightColor;
    }
    void OnMouseExit()
    {
        CursorManager.Instance.ChangeToDefault();
        this.GetComponent<Renderer>().material.color = StartColor;
    }
    private void OnMouseDown()
    {
        //call DeskTopCtrl to change state
        if(this.gameObject.tag == "Computer")
        {
            this.GetComponent<Renderer>().material.color = StartColor;
            deskcon.ChangeState(deskcon.ComputerState);
        }
        else if(this.gameObject.tag == "Notebook")
        {
            this.GetComponent<Renderer>().material.color = StartColor;
            deskcon.ChangeState(deskcon.NotebookState);
        }
        else if (this.gameObject.tag == "Bboard")
        {
            this.GetComponent<Renderer>().material.color = StartColor;
            deskcon.ChangeState(deskcon.BulboardState);
        }

    }

}
