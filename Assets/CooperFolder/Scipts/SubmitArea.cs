﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SubmitArea : MonoBehaviour
{
    public List<RectTransform> inArea = new List<RectTransform>();
    public List<string> evidences = new List<string>();
    public GameObject MaterialArea;
    
    // Start is called before the first frame update
    void Start()
    {
        FindMaterialInArea();
        MaterialArea = GameObject.Find("Material Area");
    }

    public void Update()
    {
        FindMaterialInArea();
        GetAllEvidence();
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
            }
                 
        }
    }
    

    public void RectOverlaps()
    {

        foreach (RectTransform rectTransform in MaterialArea.GetComponent<MatArea>().inArea)
        {
            Rect rect1 = new Rect(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.rect.width, rectTransform.rect.height);
            Rect rect2 = new Rect(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            
            /*RectTransform myTransform = GetComponent<RectTransform>();
            if ((rectTransform.position.x > myTransform.position.x - (myTransform.rect.width / 2)) &&
                (rectTransform.position.x < myTransform.position.x + (myTransform.rect.width / 2)) &&
                (rectTransform.position.y < myTransform.position.y + (myTransform.rect.height / 2)) &&
                (rectTransform.position.y < myTransform.position.y + (myTransform.rect.height / 2))
                )

                //if (rect1.Overlaps(rect2))
            {
                Debug.Log(rectTransform.transform.name);
                Debug.Log("will mat");

                rectTransform.transform.SetParent(this.transform);
                Debug.Log("parent to mat");
                FindMaterialInArea();
            }*/
            
            //Rect rect1 = new Rect(rectTransform.position.x, rectTransform.position.y, rectTransform.rect.width, rectTransform.rect.height);
            //Rect rect2 = new Rect(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            
            if (rect1.Overlaps(rect2))
            {
                Debug.Log(rectTransform.transform.name);
                Debug.Log("will submit");
                Debug.Log(rectTransform.parent.name);
                MaterialArea.GetComponent<MatArea>().inArea.Remove(rectTransform);
                rectTransform.transform.SetParent(this.transform);
                Debug.Log("parent to submit");
                Debug.Log(rectTransform.parent.name);
                
                //MaterialArea.GetComponent<MatArea>().FindMaterialInArea();
            }
            //FindMaterialInArea();
        }
        
    }

    private void GetAllEvidence()
    {
        evidences.Clear();
        GameObject.FindWithTag("Criminal").GetComponent<CrimialEvidence>().myMaterials.Clear();
        
        foreach (RectTransform rectTransform in inArea)
        {
            evidences.Add(rectTransform.transform.GetComponentInChildren<PrintDocument>().GetKey());
            GameObject.FindWithTag("Criminal").GetComponent<CrimialEvidence>().myMaterials.Add(rectTransform.gameObject);
        }

        GameObject.FindWithTag("Criminal").GetComponent<CrimialEvidence>().theEvidenceContained = evidences;

        /*for (int i = 0; i < transform.childCount; i++) 
        {
            if(transform.GetChild(i).gameObject.CompareTag("Material"))
            {
                evidences.Add(transform.GetChild(i).GetComponentInChildren<PrintDocument>().GetKey());
            }
                 
        }*/
    }
    
}
