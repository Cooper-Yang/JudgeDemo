using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public DeskTopCtrl deskTop;
    public Texture2D defaultCursor;
    public Texture2D clickCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    private static CursorManager _instance;

    public static CursorManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CursorManager>();
            }

            return _instance;
        }
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

    public void ChangeToClick()
    {
        Cursor.SetCursor(clickCursor, hotSpot, cursorMode);
    }

    public void ChangeToDefault()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

    private void Update()
    {
        if (deskTop.CurrentState == deskTop.ComputerState && Input.GetMouseButtonDown(0))
        {
            SoundMan.me.ClickSound();
        }
    }
}
