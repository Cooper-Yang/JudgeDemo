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

    // Update is called once per frame
    void Update()
    {
        
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

    public void RectOverlaps()
    {
        
        foreach (RectTransform rectTransform in SumbitArea.GetComponent<SubmitArea>().inArea)
        {
            Rect rect1 = new Rect(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.rect.width, rectTransform.rect.height);
            Rect rect2 = new Rect(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
            Debug.Log("called");

            if (rect1.Overlaps(rect2))
            {
                Debug.Log("overlap");

                rectTransform.transform.SetParent(this.transform);
            }
        }
        FindMaterialInArea();
    }
}
