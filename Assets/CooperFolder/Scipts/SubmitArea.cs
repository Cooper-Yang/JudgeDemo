using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEditor;

public class SubmitArea : MonoBehaviour
{
    public List<RectTransform> inArea = new List<RectTransform>();
    public List<string> evidences = new List<string>();
    public GameObject MaterialArea;
    public GameObject Button;

    public TMP_Dropdown dropDown;
    [SerializeField]
    int requireScore = 3;
    [SerializeField]
    int currentScore = 0;
    [SerializeField]
    UnityEvent toTrigger;
    //public List<GameObject> suspectList;

    // Start is called before the first frame update
    void Start()
    {
        FindMaterialInArea();
        MaterialArea = GameObject.Find("Material Area");
    }

    public void Update()
    {
        FindMaterialInArea();
        //GetAllEvidence();
    }

    public void FindMaterialInArea()
    {
        //inArea.Clear();
 
        for (int i = 0; i < transform.childCount; i++) 
        {
            //this block set the child to the list
            if(transform.GetChild(i).gameObject.CompareTag("Material"))
            {
                //Debug.Log("trans");
                inArea.Add(transform.GetChild(i).gameObject.GetComponent<RectTransform>());
                transform.GetChild(i).gameObject.transform.SetParent(transform.parent);

                //RectTransform rectTransform = transform.GetChild(i).GetComponent<RectTransform>();
                
            }
                 
        }
    }
    

    public void RectOverlaps()
    {


        /*for(int i=0;i < MaterialArea.GetComponent<MatArea>().inArea.Count; i++)
        {
            RectTransform rectTransform = MaterialArea.GetComponent<MatArea>().inArea[i];
            Rect rect1 = new Rect(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.rect.width, rectTransform.rect.height);
            Rect rect2 = new Rect(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);

            
            if (rect1.Overlaps(rect2))
            {
                //Add sound effect here

                //this line remove the gameobject from material's list
                MaterialArea.GetComponent<MatArea>().inArea.Remove(rectTransform);
                //this line add the gameobject into our list, in a magical way, check the methods find material in area
                rectTransform.transform.SetParent(this.transform);
                //this two line add the key to the current crinimal
                SuspectList.Instance.susList[dropDown.value].GetComponent<CrimialEvidence>().theEvidenceContained.Add
                    (rectTransform.transform.GetComponentInChildren<PrintDocument>().GetKey());
                //this line add the gameobject to the current crinimals
                SuspectList.Instance.susList[dropDown.value].GetComponent<CrimialEvidence>().myMaterials.Add(rectTransform.gameObject);
            }

        }*/
        
    }

    private void GetAllEvidence()
    {
        //this method is no longer used
        evidences.Clear();
        SuspectList.Instance.susList[dropDown.value].GetComponent<CrimialEvidence>().myMaterials.Clear();

        foreach (RectTransform rectTransform in inArea)
        {
            if (rectTransform.gameObject.activeInHierarchy)
            {
                evidences.Add(rectTransform.transform.GetComponentInChildren<PrintDocument>().GetKey());
                SuspectList.Instance.susList[dropDown.value].GetComponent<CrimialEvidence>().myMaterials.Add(rectTransform.gameObject);
            }
            
        }

        //GameObject.FindWithTag("Criminal").GetComponent<CrimialEvidence>().theEvidenceContained = evidences;
        SuspectList.Instance.susList[dropDown.value].GetComponent<CrimialEvidence>().theEvidenceContained = evidences;

        /*for (int i = 0; i < transform.childCount; i++) 
        {
            if(transform.GetChild(i).gameObject.CompareTag("Material"))
            {
                evidences.Add(transform.GetChild(i).GetComponentInChildren<PrintDocument>().GetKey());
            }
                 
        }*/
    }

    public void HandleInputData()
    {
        //This methods controlls the active states of the materials belongs to different criminals
        int k = dropDown.value;
        foreach (GameObject i in SuspectList.Instance.susList)
        {
            foreach (GameObject gO in i.GetComponent<CrimialEvidence>().myMaterials)
            {
                gO.SetActive(false);
            }


        }

        foreach (GameObject gO in SuspectList.Instance.susList[k].GetComponent<CrimialEvidence>().myMaterials)
        {
            gO.SetActive(true);
        }
        
    }

    public void SubmitReport()
    {
        if (SuspectList.Instance.dropDown.options.Count <= 0)
        {
            return;
        }
        /*string caseID = suspectList[dropDown.value].name;
        Debug.Log(caseID);
        foreach (OneCase cm in FindObjectsOfType<OneCase>())
        {
            if (cm.caseID.Equals(caseID))
            {
                cm.submitReport(suspectList[dropDown.value].GetComponent<CrimialEvidence>().theEvidenceContained);
                Debug.Log("submitted");
                suspectList[dropDown.value].GetComponent<CrimialEvidence>().theEvidenceContained.Clear();
                foreach (GameObject gO in suspectList[dropDown.value].GetComponent<CrimialEvidence>().myMaterials)
                {
                    Destroy(gO);
                }
                break;
            }
        }*/
        CrimialEvidence crimeData = SuspectList.Instance.susList[dropDown.value].GetComponent<CrimialEvidence>();
        int score = 0;
        foreach (string sub in crimeData.theEvidenceContained)
        {
            foreach (string ans in crimeData.theEvidenceComparedTo)
            {
                if (ans.Equals(sub))
                {
                    score++;
                }
            }
        }
        Debug.Log(score + " evidences matches !");
        inArea.Clear();
        currentScore += crimeData.score;
        if (currentScore >= requireScore)
        {
            currentScore = 0;
            toTrigger.Invoke();
        }
        if (score >= crimeData.theEvidenceComparedTo.Count)
        {
            crimeData.goodend.Invoke();
        }
        else
        {
            crimeData.badend.Invoke();
        }
        SuspectList.Instance.RemoveSuspect(crimeData.gameObject);
        
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("enter");
        //print(collision.name);
        if (collision.gameObject.GetComponent<PrintDocument>()&&!inArea.Contains(collision.GetComponent<RectTransform>()))
        {
            //SoundMan.me.EvidenceDropSound(Vector3.zero);
            MaterialArea.GetComponent<MatArea>().inArea.Remove(collision.GetComponent<RectTransform>());
            collision.transform.SetParent(this.transform);
            //this two line add the key to the current crinimal
            SuspectList.Instance.susList[dropDown.value].GetComponent<CrimialEvidence>().theEvidenceContained.Add
                   (collision.transform.GetComponentInChildren<PrintDocument>().GetKey());
            //this line add the gameobject to the current crinimals
            SuspectList.Instance.susList[dropDown.value].GetComponent<CrimialEvidence>().myMaterials.Add(collision.gameObject);
        }
    }

    public void endButton()
    {
        Button.SetActive(true);
    }
}
