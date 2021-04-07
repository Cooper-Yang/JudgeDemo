using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimialEvidence : MonoBehaviour
{
    public List<string> theEvidenceComparedTo;
    public List<string> theEvidenceContained;

    public List<GameObject> myMaterials = new List<GameObject>();

    private void OnDestroy()
    {
        for(int i=0; i < myMaterials.Count; i++)
        {
            Destroy(myMaterials[i]);
        }
    }
}
