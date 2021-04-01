using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SubmitArea : MonoBehaviour
{
    public List<RectTransform> inArea = new List<RectTransform>();
    public GameObject MaterialArea;
    
    // Start is called before the first frame update
    void Start()
    {
        FindMaterialInArea();
        MaterialArea = GameObject.Find("Material Area");
    }
    

    void FindMaterialInArea()
    {
        inArea.Clear();
 
        for (int i = 0; i < transform.childCount; i++) 
        {
            if(transform.GetChild(i).gameObject.CompareTag("Material"))
            {
                inArea.Add(transform.GetChild(i).gameObject.GetComponent<RectTransform>());
            }
                 
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter submit");
        other.transform.SetParent(transform);
        FindMaterialInArea();
    }

    public void RectOverlaps()
    {

        foreach (RectTransform rectTransform in MaterialArea.GetComponent<MatArea>().inArea)
        {
            //Rect rect1 = new Rect(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.rect.width, rectTransform.rect.height);
            //Rect rect2 = new Rect(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            Rect rect1 = new Rect(rectTransform.position.x, rectTransform.position.y, rectTransform.rect.width, rectTransform.rect.height);
            Rect rect2 = new Rect(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            
            if (rect1.Overlaps(rect2))
            {
                Debug.Log("will submit");
                Debug.Log(rectTransform.parent.name);
                rectTransform.transform.SetParent(this.transform);
                Debug.Log("parent to submit");
                Debug.Log(rectTransform.parent.name);
                FindMaterialInArea();
            }
        }
        
    }
    
}
