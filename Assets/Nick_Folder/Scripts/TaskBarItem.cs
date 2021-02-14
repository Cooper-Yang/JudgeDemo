using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBarItem : MonoBehaviour
{
    Image myImage;
    public Image CorrespondingWindow;

    public Color baseColor;
    public Color openColor;

    private void Start()
    {
        myImage = this.GetComponent<Image>();
    }
    private void Update()
    {
        if (CorrespondingWindow.IsActive())
        {
            if (myImage.color != openColor)
                myImage.color = openColor;
        }
        else
        {
            if (myImage.color != baseColor)
                myImage.color = baseColor;
        }
    }

}
