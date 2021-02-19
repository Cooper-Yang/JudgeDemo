using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    public DeskTopCtrl deskcon;
    private Color startcolor;

    private void Start()
    {
        this.GetComponent<Renderer>().material.color = startcolor;
    }

    void OnMouseEnter()
    {
        startcolor = Color.white;
        this.GetComponent<Renderer>().material.color = Color.yellow;
    }
    void OnMouseExit()
    {
        this.GetComponent<Renderer>().material.color = startcolor;
    }
    private void OnMouseDown()
    {
        //call DeskTopCtrl to change state
        if(this.gameObject.tag == "Computer")
        {
            this.GetComponent<Renderer>().material.color = startcolor;
            deskcon.ChangeState(deskcon.ComputerState);
        }
        else if(this.gameObject.tag == "Notebook")
        {

        }

    }

}
