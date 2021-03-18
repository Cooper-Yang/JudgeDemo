using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompUIManager : MonoBehaviour
{
    public Image EmailWindow;
    public Image ConsoleWindow;


    public void ToggleWindow(Image window)
    {
        if (window.IsActive())
        {
            CloseWindow(window);
        }
        else
        {
            OpenWindow(window);
        }
    }

    public void CloseWindow(Image window)
    {
        window.gameObject.SetActive(false);
    }

    public void OpenWindow(Image window)
    {
        RectTransform rt = window.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, 0);
        // 
        // RESET POSITION ON OPEN
        //

        window.gameObject.SetActive(true);
        window.transform.SetAsLastSibling();
    }
}
