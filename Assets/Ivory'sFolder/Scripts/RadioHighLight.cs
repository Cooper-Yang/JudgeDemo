using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioHighLight : MonoBehaviour
{
    //public DeskTopCtrl deskcon;
    private Color StartColor = Color.white;
    private Color HightlightColor = Color.yellow;
    public GameObject radioMan;

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

    void OnMouseDown()
    {
        radioMan.GetComponent<RadioControl>().PauseOrResume();
    }
}
