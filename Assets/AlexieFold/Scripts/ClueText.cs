using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers
using TMPro;

public class ClueText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ // Extends the pointer handlers
    public bool choosen = false;
    private Color temp_c;

    public void OnPointerEnter(PointerEventData eventData)
    {
        temp_c = GetComponent<TextMeshProUGUI>().color;
        GetComponent<TextMeshProUGUI>().color = new Color(0.3f,0.3f,0.3f); // Changes the colour of the text
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = temp_c; // Changes the colour of the text
    }

    public void changeColor()
    {
        GetComponent<TextMeshProUGUI>().color = temp_c;
    }
}
