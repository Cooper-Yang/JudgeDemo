using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    public Button someButton;

    void OnEnable()
    {
        //Register Button Events
        someButton.onClick.AddListener(() => buttonCallBack());
    }

    private void buttonCallBack()
    {
        TimedEvents TE = FindObjectOfType<TimedEvents>();
        TE.triggerEvent_Wait(2, 60);
    }

    void OnDisable()
    {
        //Un-Register Button Events
        someButton.onClick.RemoveAllListeners();
    }
}
