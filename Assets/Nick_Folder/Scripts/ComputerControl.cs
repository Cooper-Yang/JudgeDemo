using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComputerControl : MonoBehaviour
{
    public static ComputerControl Control;
    public int LinesOnScreen;
    [Space(10)]
    public File startFile;
    public File currentFile;

    private void Start()
    {
        if (startFile)
            LoadFile(startFile);
    }


    private void LoadFile(File F)
    {
        if (currentFile != F)
        {
            currentFile = F;
            F.Open();
        }
        else
        {
            // File Already Open
        }
    }



}

