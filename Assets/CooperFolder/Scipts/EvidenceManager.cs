using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvidenceManager : MonoBehaviour
{
    public List<GameObject> suspectList;

    private Dictionary<string, List<string>> theEvidence; //evidence we are comparing to, the index should be the name of criminal, the evidence list is now a string list, but it can be changed
    
    public TMP_Dropdown dropDown;
    
    private static EvidenceManager _instance;

    public static EvidenceManager Instance => _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        
    }

    private void Start()
    {
        theEvidence = new Dictionary<string, List<string>>();
        /*suspectList.Clear();
        foreach (GameObject gO in GameObject.FindGameObjectsWithTag("Criminal"))
        {
            suspectList.Add(gO);
        }*/
        
        //suspectList = GameObject.FindGameObjectsWithTag("Criminal");
        /*foreach (GameObject i in suspectList)
        {
            theEvidence.Add(i.name,i.GetComponent<CrimialEvidence>().theEvidenceComparedTo);
        }*/
    }

    public void HandleInputData()
    {
        int k = dropDown.value;
        foreach (GameObject i in suspectList)
        {
            foreach (GameObject gO in i.GetComponent<CrimialEvidence>().myMaterials)
            {
                gO.SetActive(false);
            }

            
        }
        
        foreach (GameObject gO in suspectList[k].GetComponent<CrimialEvidence>().myMaterials)
        {
            gO.SetActive(true);
        }
        //suspectList[k].SetActive(true);
    }

    public void CompareEvidence()
    {
        int k = dropDown.value;
        List<string> thisEvidence = new List<string>();
        int containedEvidenceNumber = 0;  //this number is used to calculate how right or wrong the evidence submitted are
        if (theEvidence.TryGetValue(suspectList[k].name,out thisEvidence))
        {
            foreach (string s in thisEvidence)
            {
                foreach (string g in suspectList[k].GetComponent<CrimialEvidence>().theEvidenceContained)
                {
                    if (s.Equals(g))
                    {
                        containedEvidenceNumber += 1;
                    }
                }
            }
        }
        
    }

    public void SubmitReport()
    {
        string caseID = suspectList[dropDown.value].name;
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
        }
    }
}
