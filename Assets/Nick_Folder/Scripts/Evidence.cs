using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence
{
    string key;

    public void SetKey(string s) { key = s; }
    public string GetKey() { return key; }
}




public class EvidenceGroup
{
    List<Evidence> thisGroup = new List<Evidence>();
    string key;
    public string GetKey() { return key; }
    public EvidenceGroup(string keyword)
    {
        key = keyword;
        foreach(Evidence e in thisGroup)
        {
            e.SetKey(keyword);
        }
    }

    public int GetCount() { return thisGroup.Count; }
    public List<Evidence> GetEvidenceList() { return thisGroup; }

    public void AddEvidence(Evidence e)
    {
        thisGroup.Add(e);
    }

    public void AddGroup(EvidenceGroup group)
    {
        foreach (Evidence e in group.GetEvidenceList())
        {
            thisGroup.Add(e);
        }
    }

    public string[] GetKeys()
    {
        string[] arr = new string[thisGroup.Count];
        int count = 0;
        foreach (Evidence e in thisGroup)
        {
            arr[count] = e.GetKey();
            count++;
        }
        return arr;
    }
}
