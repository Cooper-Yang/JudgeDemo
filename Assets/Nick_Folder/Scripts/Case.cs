using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case
{
    public enum Crime { None, Forgery, Theft, Arson, Tresspassing, Assault, Murder, Purjury, Insurrection, Treason };

    string caseName;
    string caseKey;
    int dateOpened;
    int dateDue;
    EvidenceGroup evidenceNeeded;
    EvidenceGroup evidenceHolding;
    Crime crimeNeeded;
    Crime secondCrime;
    List<Crime> crimesSelected = new List<Crime>();
    Case nextCase;

    string solutionKey;

    public Case(string name, string nameKey, int date, EvidenceGroup evidence, Crime crime, Crime crime2)
    {
        caseName = name;
        caseKey = nameKey;
        dateDue = date;
        //evidenceNeeded = evidence;
        //crimesNeeded.Add(crime);

        solutionKey = nameKey;
        foreach (string s in evidence.GetKeys())
        { solutionKey += s; }
        // Pre-set in CaseManager -- you compare evidence you find with the strings in the key
        // So a SOLUTION KEY might be   "XiaoWangForgeryTreason" ex: "NameCrime"
        // and a Submit KEY might be    "ZhangLouAssaultMurder" if you are looking at the ZhangLou case and have dragged in evidence of "Assault" and "Murder"
            // If SolutionKey and SubmitKey match, the case is solved!
    }

    public EvidenceGroup GetNeededEvidence()
    { return evidenceNeeded; }

    /*Case NextCase()
    {
        return CaseManager.SparrowCase1;
    }*/
}
