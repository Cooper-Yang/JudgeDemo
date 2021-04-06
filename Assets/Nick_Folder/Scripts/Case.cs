using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    public enum Crime { Forgery, Theft, Arson, Tresspassing, Assault, Murder, Purjury, Insurrection, Treason };

    string caseName;
    int dateOpened;
    int dateDue;
    List<Evidence> evidenceNeeded;
    List<Evidence> evidenceCurrent;
    List<Crime> crimesNeeded;
    List<Crime> crimesCurrent;
    Case nextCase;

    public Case(string name, int date, List<Evidence> evidence, Crime crime)
    {
        caseName = name;
        dateDue = date;
        evidenceNeeded = evidence;
        crimesNeeded.Add(crime);
    }

    public Case(string name, int date, List<Evidence> evidence, List<Crime> crimes)
    {
        caseName = name;
        dateDue = date;
        evidenceNeeded = evidence;
        foreach (Crime c in crimes)
            crimesNeeded.Add(c);
    }

    /*Case NextCase()
    {
        return CaseManager.SparrowCase1;
    }*/
}
