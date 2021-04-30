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
            SoundMan.me.ComputerSound(Vector3.zero);
            this.GetComponent<Renderer>().material.color = StartColor;
            deskcon.ChangeState(deskcon.ComputerState);
        }
        else if(this.gameObject.tag == "Notebook")
        {
            SoundMan.me.NewspaperSound(Vector3.zero);
            this.GetComponent<Renderer>().material.color = StartColor;
            deskcon.ChangeState(deskcon.NotebookState);
        }
        else if (this.gameObject.tag == "Bboard")
        {
            SoundMan.me.BoardSound(Vector3.zero);
            this.GetComponent<Renderer>().material.color = StartColor;
            deskcon.ChangeState(deskcon.BulboardState);
        }

    }

}
