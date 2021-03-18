using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneCase : TimedEvents
{
    public string caseID;
    public List<string> requiredEvidences;
    public List<int> winConditions;//different about of matched evidences triggers different ending

    public List<tEvent> endings;//the ending stored to be triggered

    public void submitReport(List<string> submitted)
    {
        int score = 0;
        foreach(string sub in submitted)
        {
            foreach(string ans in requiredEvidences)
            {
                if (ans.Equals(sub))
                {
                    score++;
                }
            }
        }
        Debug.Log(score + " evidence success");
        events.Add(endings[winConditions[score]]);
        Debug.Log("Triggered ending "+ winConditions[score]);
        triggerEvent_Next(events.Count - 1, 1);
    }
}
