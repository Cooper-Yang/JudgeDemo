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
            }
                 
        }
    }

    public void RectOverlaps()
    {
        
        /*foreach (RectTransform rectTransform in SumbitArea.GetComponent<SubmitArea>().inArea)
        {
            Rect rect1 = new Rect(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.rect.width, rectTransform.rect.height);
            Rect rect2 = new Rect(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);/*
            
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
            }/
            
            //Rect rect1 = new Rect(rectTransform.position.x, rectTransform.position.y, rectTransform.rect.width, rectTransform.rect.height);
            //Rect rect2 = new Rect(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            
            if (rect1.Overlaps(rect2))
            {
                Debug.Log(rectTransform.transform.name);
                Debug.Log("will mat");
                SumbitArea.GetComponent<SubmitArea>().inArea.Remove(rectTransform);
                rectTransform.transform.SetParent(this.transform);
                Debug.Log("parent to mat");
                
                //SumbitArea.GetComponent<SubmitArea>().FindMaterialInArea();
            }
            //FindMaterialInArea();
        }*/
        for(int i=0;i< SumbitArea.GetComponent<SubmitArea>().inArea.Count; i++)
        {
            RectTransform rectTransform= SumbitArea.GetComponent<SubmitArea>().inArea[i];
            Rect rect1 = new Rect(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.rect.width, rectTransform.rect.height);
            Rect rect2 = new Rect(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);/*
            
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
                Debug.Log("will mat");
                SumbitArea.GetComponent<SubmitArea>().inArea.Remove(rectTransform);
                rectTransform.transform.SetParent(this.transform);
                Debug.Log("parent to mat");
                
                //SumbitArea.GetComponent<SubmitArea>().FindMaterialInArea();
            }
            //FindMaterialInArea();
        }
        
    }

}
