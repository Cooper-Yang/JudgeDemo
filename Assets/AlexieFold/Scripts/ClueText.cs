using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers
using TMPro;

public class ClueText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{ // Extends the pointer handlers
    public bool choosen = false;
    private EviToRep eviToRep;
    void Start()
    {
        eviToRep = GameObject.Find("NoteManager").GetComponent<EviToRep>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = Color.gray; // Changes the colour of the text
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (choosen)
        {
            GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().color = Color.black; // Changes the colour of the text
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!choosen)
        {
            choosen = true;
            eviToRep.choosenList.Add(GetComponent<TextMeshProUGUI>().text);
            for(int i = 0; i < eviToRep.choosenList.Count; i++)
                Debug.Log(eviToRep.choosenList[i]);
            if (eviToRep.choosenList.Count == 0)
                Debug.Log("choosenList is empty");
        }
        else
        {
            choosen = false;
            eviToRep.choosenList.Remove(GetComponent<TextMeshProUGUI>().text);
        }
    }
}
