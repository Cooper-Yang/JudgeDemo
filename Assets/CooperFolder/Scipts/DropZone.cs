using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour//, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    /*public TextMeshProUGUI evidenceKeyword;

    private void Awake()
    {
        evidenceKeyword = GetComponentInParent<TextMeshProUGUI>();
        evidenceKeyword.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData) {

        if (eventData.pointerDrag == null)
            return;

        if (transform.childCount > 1)
        {
            return;
        }
        
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
   
        if (d != null)
        {
            d.placeHolderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {

        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeHolderParent == this.transform)
        {
            d.parentToReturnTo = GameObject.Find("Canvas - Board").transform;
            d.placeHolderParent = d.parentToReturnTo;
            //d.GetBack();
        }
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log(eventData.pointerDrag.name + " dropped on " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        
        /*if (transform.childCount > 1)
        {
            d.GetBack();
            //d.parentToReturnTo = d.parentToReturnTo;
            return;
        }*/
        
       /* if (d != null) {
            d.parentToReturnTo = this.transform;
            if (gameObject.CompareTag("Container"))
            {
                evidenceKeyword.text = d.GetComponentInChildren<PrintDocument>().GetKey();
                transform.parent.parent.parent.GetComponent<CrimialEvidence>().theEvidenceContained.Add(evidenceKeyword.text);  //add the name of evidence
                d.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }*/
    
}
