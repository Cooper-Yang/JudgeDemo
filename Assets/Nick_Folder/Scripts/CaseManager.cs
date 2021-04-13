using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    static EvidenceGroup forgeryEvidence =  new EvidenceGroup("forgery"); // fake passports, fake ticket, fake money
    static EvidenceGroup theftEvidence =    new EvidenceGroup("theft");
    static EvidenceGroup arsonEvidence =    new EvidenceGroup("arson");
    static EvidenceGroup tresspassingEvidence = new EvidenceGroup("tresspassing");
    static EvidenceGroup assaultEvidence =  new EvidenceGroup("assault");
    static EvidenceGroup murderEvidence =   new EvidenceGroup("murder");
    static EvidenceGroup purjuryEvidence =  new EvidenceGroup("purjury");
    static EvidenceGroup insurrectionEvidence = new EvidenceGroup("insurrection");
    static EvidenceGroup treasonEvidence =  new EvidenceGroup("treason");

    List<Case> OpenCases;
    List<Case> ClosedCases;
    List<Case> TotalCases;

    Case case1_0 = new Case("Person 0", "introEmail",   2,  forgeryEvidence,    Case.Crime.Forgery, 0); // tutorial email --> last 0 param means no second Crime
    Case case1_1 = new Case("Person 1", "introCustoms", 3,  forgeryEvidence,    Case.Crime.Forgery, 0); // tutorial customs
    Case case1_2 = new Case("Person 2", "introFinance", 4,  theftEvidence,      Case.Crime.Theft,   0);   // tutorial financial
    Case case1_3 = new Case("Person 3", "introCrime",   4,  murderEvidence,     Case.Crime.Murder,  0);  // tutorial law enforcement

    Case case2_1 = new Case("Person 4", "keyword",  5,  CombineEvidence(assaultEvidence, murderEvidence),   Case.Crime.Assault, Case.Crime.Murder);  // more complicated cases
    Case case2_2 = new Case("Person 5", "keyword",  6,  CombineEvidence(forgeryEvidence, treasonEvidence),  Case.Crime.Forgery, Case.Crime.Treason); // have 2 evidence groups since there are 2 crimes
    Case case2_3 = new Case("Xiao Wang", "keyword", 7,  CombineEvidence(forgeryEvidence, treasonEvidence),  Case.Crime.Forgery, Case.Crime.Treason); 

    Case case3_1 = new Case("Person 7", "keyword",  8,  CombineEvidence(forgeryEvidence, treasonEvidence),  Case.Crime.Forgery, Case.Crime.Treason);
    Case case3_2 = new Case("Person 8", "keyword",  9,  CombineEvidence(forgeryEvidence, treasonEvidence),  Case.Crime.Forgery, Case.Crime.Treason);
    Case case3_3 = new Case("Person 9", "keyword",  10, CombineEvidence(forgeryEvidence, treasonEvidence),  Case.Crime.Forgery, Case.Crime.Treason);

    //Case SparrowCase1 = new Case()

    private static EvidenceGroup CombineEvidence(EvidenceGroup evidence1, EvidenceGroup evidence2)
    {
        EvidenceGroup group = new EvidenceGroup(evidence1.GetKey() + evidence2.GetKey());
        group.AddGroup(evidence1);
        group.AddGroup(evidence2);
        return group;
    }

    /*private static List<Case.Crime> Crimes(Case.Crime crime1, Case.Crime crime2)
    {
        List<Case.Crime> crimes = new List<Case.Crime>();
        crimes.Add(crime1);
        crimes.Add(crime2);
        return crimes;
    }*/

}
