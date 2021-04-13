using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case
{
    public enum Crime { None, Forgery, Theft, Arson, Tresspassing, Assault, Murder, Purjury, Insurrection, Treason };

    string caseName;
    int dateOpened;
    int dateDue;
    string[] evidenceNeeded;
    string[] evidenceHolding;
    Crime crimeNeeded;
    Crime secondCrime;
    List<Crime> crimesSelected = new List<Crime>();
    Case nextCase;

    string solutionKey;

    public Case(string name, string key, int date, string[] keys, Crime crime, Crime crime2)
    {
        caseName = name;
        dateDue = date;
        //evidenceNeeded = evidence;
        //crimesNeeded.Add(crime);

        solutionKey = key;
        // Pre-set in CaseManager -- you compare evidence you find with the strings in the key
        // So a SOLUTION KEY might be   "XiaoWangForgeryTreason" ex: "NameCrime"
        // and a Submit KEY might be    "ZhangLouAssaultMurder" if you are looking at the ZhangLou case and have dragged in evidence of "Assault" and "Murder"
            // If SolutionKey and SubmitKey match, the case is solved!
    }

    public string[] GetNeededEvidence()
    { return evidenceNeeded; }
    public string[] GetHoldingEvidence()
    { return evidenceHolding; }
    public string GetSolutionKey()
    { return solutionKey; }

    /*Case NextCase()
    {
        return CaseManager.SparrowCase1;
    }*/
}
