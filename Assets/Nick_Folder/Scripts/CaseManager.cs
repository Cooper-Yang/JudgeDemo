using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    static string[] forgeryKeys =   { "FakeArtwork",    "FakeDocument",         "FakeMoney" };
    static string[] theftKeys =     { "StolenProperty", "StolenMoney",          "IllegalOwnership" };
    static string[] arsonKeys =     { "FireDamage",     "DestroyedProperty",    "DestroyedMoney" };
    static string[] tresspassingKeys = { "",            "",                     "" };
    static string[] assaultKeys =   { "injuredVictim",  "biologicalEvidence",   "ownsWeapon",       "AtCrimescene" };
    static string[] murderKeys = { "deadVictim", "biologicalEvidence", "ownsWeapon", "AtCrimescene", "ballisticsReport" };
    static string[] purjuryKeys =   { "officialLying",  "defamingGovernment",   "spreadFalseInformation" };
    static string[] insurrectionKeys = { "illegalOwnership", "protestNonviolent",    "protestViolent", "defamingGovernment" };
    static string[] treasonKeys =   { "fleeingCountry", "SparrowConnection",    "holdingClassifiedDoc",  "distributingClassifiedDoc" };

    /*static EvidenceGroup forgeryEvidence = new EvidenceGroup(forgeryKeys); // fake passports, fake ticket, fake money
    static EvidenceGroup theftEvidence =    new EvidenceGroup(theftKeys);
    static EvidenceGroup arsonEvidence =    new EvidenceGroup(arsonKeys);
    static EvidenceGroup tresspassingEvidence = new EvidenceGroup(tresspassingKeys);
    static EvidenceGroup assaultEvidence =  new EvidenceGroup(assaultKeys);
    static EvidenceGroup murderEvidence =   new EvidenceGroup(murderKeys);
    static EvidenceGroup purjuryEvidence =  new EvidenceGroup(purjuryKeys);
    static EvidenceGroup insurrectionEvidence = new EvidenceGroup(insurrectionKeys);
    static EvidenceGroup treasonEvidence =  new EvidenceGroup(tresspassingKeys);*/

    List<Case> OpenCases;
    List<Case> ClosedCases;
    List<Case> TotalCases;

    Case case1_0 = new Case("Person 0",  "introEmail",   2,  tresspassingKeys,  Case.Crime.Forgery, 0);  // tutorial email --> last 0 param means no second Crime
    Case case1_1 = new Case("Person 1",  "introCustoms", 3,  forgeryKeys,       Case.Crime.Forgery, 0);  // tutorial customs
    Case case1_2 = new Case("Person 2",  "introFinance", 4,  theftKeys,         Case.Crime.Theft,   0);  // tutorial financial
    Case case1_3 = new Case("Person 3",  "introCrime",   4,  assaultKeys,       Case.Crime.Murder,  0);  // tutorial law enforcement

    Case case2_1 = new Case("Person 4",  "keyword",  5,  CombineKeys(assaultKeys, murderKeys),  Case.Crime.Assault, Case.Crime.Murder);  // more complicated cases
    Case case2_2 = new Case("Person 5",  "keyword",  6,  CombineKeys(forgeryKeys, treasonKeys), Case.Crime.Forgery, Case.Crime.Treason); // have 2 evidence groups since there are 2 crimes
    Case case2_3 = new Case("Xiao Wang", "keyword", 7,   CombineKeys(arsonKeys, assaultKeys),   Case.Crime.Forgery, Case.Crime.Treason); 

    //Case case3_1 = new Case("Person 7", "keyword",  8,  CombineEvidence(forgeryEvidence, treasonEvidence),  Case.Crime.Forgery, Case.Crime.Treason);
    //Case case3_2 = new Case("Person 8", "keyword",  9,  CombineEvidence(forgeryEvidence, treasonEvidence),  Case.Crime.Forgery, Case.Crime.Treason);
    //Case case3_3 = new Case("Person 9", "keyword",  10, CombineEvidence(forgeryEvidence, treasonEvidence),  Case.Crime.Forgery, Case.Crime.Treason);

    //Case SparrowCase1 = new Case()

    /*private static EvidenceGroup CombineEvidence(EvidenceGroup evidence1, EvidenceGroup evidence2)
    {
        EvidenceGroup group = new EvidenceGroup(evidence1.GetKey() + evidence2.GetKey());
        group.AddGroup(evidence1);
        group.AddGroup(evidence2);
        return group;
    }*/

    private static string[] CombineKeys(string[] a, string[] b)
    {
        string[] c = new string[a.Length+b.Length];
        int i = 0;
        foreach (string s in a)
        { c[i] = s; i++; }
        foreach (string s in b)
        { c[i] = s; i++; }
        return c;
    }

    /*private static List<Case.Crime> Crimes(Case.Crime crime1, Case.Crime crime2)
    {
        List<Case.Crime> crimes = new List<Case.Crime>();
        crimes.Add(crime1);
        crimes.Add(crime2);
        return crimes;
    }*/

    // Compares NeededEvidence with HoldingEvidence for any case -- checks how many keywords match
    private int CalculateCaseScore(Case C)
    {
        int score = 0;
        string[] solutionKeys = C.GetNeededEvidence();
        string[] evidence = C.GetHoldingEvidence();

        List<string> evidenceKeys = new List<string>();
        List<string> evidenceNames = new List<string>();
        foreach (string s in evidence)
        {
            string[] cut = s.Split('_');
            evidenceKeys.Add(cut[0]);
            evidenceNames.Add(cut[1]);
        }

        foreach(string key in solutionKeys)
        {
            foreach (string evi in evidence)
            {
                if (evi.Contains(key) && evi.Contains(C.GetSolutionKey()))
                {
                    score++;
                }
            }
        }

        return score;
        //return (int)(score / solutionKeys.Length)*100;
    }

}
