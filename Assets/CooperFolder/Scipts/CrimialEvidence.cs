﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CrimialEvidence : MonoBehaviour
{
    public List<string> theEvidenceComparedTo;
    public List<string> theEvidenceContained;

    public List<GameObject> myMaterials = new List<GameObject>();
    public UnityEvent goodend;
    public UnityEvent badend;
    public int score = 1;
    private void OnDestroy()
    {
        for(int i=0; i < myMaterials.Count; i++)
        {
            Destroy(myMaterials[i]);
        }
    }

    public void clear()
    {
        for (int i = 0; i < myMaterials.Count; i++)
        {
            Destroy(myMaterials[i]);
        }
        theEvidenceContained.Clear();
        myMaterials.Clear();
    }
}
