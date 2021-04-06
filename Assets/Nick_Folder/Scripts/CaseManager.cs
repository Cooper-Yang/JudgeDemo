using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    static List<Evidence> forgeryEvidence; // fake passports, fake ticket, fake money
    static List<Evidence> theftEvidence;
    static List<Evidence> arsonEvidence;
    static List<Evidence> tresspassingEvidence;
    static List<Evidence> assaultEvidence;
    static List<Evidence> murderEvidence;
    static List<Evidence> purjuryEvidence;
    static List<Evidence> insurrectionEvidence;
    static List<Evidence> treasonEvidence;

    List<Case> OpenCases;
    List<Case> ClosedCases;
    List<Case> TotalCases;

    Case case1_0 = new Case("Person 0", 2,  forgeryEvidence,    Case.Crime.Forgery); // tutorial email
    Case case1_1 = new Case("Person 1", 2,  forgeryEvidence,    Case.Crime.Forgery); // tutorial customs
    Case case1_2 = new Case("Person 2", 3,  theftEvidence,      Case.Crime.Theft);   // tutorial financial
    Case case1_3 = new Case("Person 3", 3,  murderEvidence,     Case.Crime.Murder);  // tutorial law enforcement

    Case case2_1 = new Case("Person 4",  5, CombineEvidence(assaultEvidence, murderEvidence),   Crimes(Case.Crime.Assault, Case.Crime.Murder));
    Case case2_2 = new Case("Xiao Wang", 6, CombineEvidence(forgeryEvidence, treasonEvidence),  Crimes(Case.Crime.Forgery, Case.Crime.Treason));
    Case case2_3 = new Case("Xiao Wang", 6, CombineEvidence(forgeryEvidence, treasonEvidence),  Crimes(Case.Crime.Forgery, Case.Crime.Treason));

    Case case3_1 = new Case("Xiao Wang", 6, CombineEvidence(forgeryEvidence, treasonEvidence), Crimes(Case.Crime.Forgery, Case.Crime.Treason));
    Case case3_2 = new Case("Xiao Wang", 6, CombineEvidence(forgeryEvidence, treasonEvidence), Crimes(Case.Crime.Forgery, Case.Crime.Treason));
    Case case3_3 = new Case("Xiao Wang", 6, CombineEvidence(forgeryEvidence, treasonEvidence), Crimes(Case.Crime.Forgery, Case.Crime.Treason));

    //Case SparrowCase1 = new Case()

    
    private static List<Evidence> CombineEvidence(List<Evidence> evidence1, List<Evidence> evidence2)
    {
        List<Evidence> newEvidence = new List<Evidence>();
        foreach (Evidence e in evidence1)
            newEvidence.Add(e);

        return newEvidence;
    }

    private static List<Case.Crime> Crimes(Case.Crime crime1, Case.Crime crime2)
    {
        List<Case.Crime> crimes = new List<Case.Crime>();
        crimes.Add(crime1);
        crimes.Add(crime2);
        return crimes;
    }

}
