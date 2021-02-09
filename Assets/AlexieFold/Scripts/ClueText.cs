using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers

public class ClueText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{ // Extends the pointer handlers
    public bool choosen = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Text>().color = Color.gray; // Changes the colour of the text
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (choosen)
        {
            GetComponent<Text>().color = Color.red;
        }
        else
        {
            GetComponent<Text>().color = Color.black; // Changes the colour of the text
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!choosen)
        {
            choosen = true;
        }
        else
        {
            choosen = false;
        }
    }
}
