using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatArea : MonoBehaviour
{
    public List<RectTransform> inArea = new List<RectTransform>();
    public GameObject SumbitArea;
    
    // Start is called before the first frame update
    void Start()
    {
        FindMaterialInArea();
        SumbitArea = GameObject.Find("Submit Area");
    }

    public void Update()
    {
        FindMaterialInArea();
    }

    public void FindMaterialInArea()
    {
        //inArea.Clear();
 
        for (int i = 0; i < transform.childCount; i++) 
        {
            if(transform.GetChild(i).gameObject.CompareTag("Material"))
            {
                inArea.Add(transform.GetChild(i).gameObject.GetComponent<RectTransform>());
                transform.GetChild(i).gameObject.transform.SetParent(transform.parent);
                //RectTransform rectTransform = transform.GetChild(i).GetComponent<RectTransform>();
                
            }
                 
        }
    }

    public void RectOverlaps()
    {
        
        for(int i=0;i< SumbitArea.GetComponent<SubmitArea>().inArea.Count; i++)
        {
            RectTransform rectTransform= SumbitArea.GetComponent<SubmitArea>().inArea[i];
            Rect rect1 = new Rect(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.rect.width, rectTransform.rect.height);
            Rect rect2 = new Rect(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            
            
            
            if (rect1.Overlaps(rect2))
            {

                //Add sound effect here

                //this line remove the gameobject from the submitarea
                SumbitArea.GetComponent<SubmitArea>().inArea.Remove(rectTransform);
                //this line add the gameobject into this list, check FindMaterialInArea() for further details  
                rectTransform.transform.SetParent(this.transform);
                //this line remove the key from the current crinimal
                SuspectList.Instance.susList[SumbitArea.GetComponent<SubmitArea>().dropDown.value].GetComponent<CrimialEvidence>().theEvidenceContained.Remove
    (rectTransform.transform.GetComponentInChildren<PrintDocument>().GetKey());
                //thie line remove the gameobject from the current criminal
                SuspectList.Instance.susList[SumbitArea.GetComponent<SubmitArea>().dropDown.value].GetComponent<CrimialEvidence>().myMaterials.Remove(rectTransform.gameObject);
            }

        }
        
    }

}
