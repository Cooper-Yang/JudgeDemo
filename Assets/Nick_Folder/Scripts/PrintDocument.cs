using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintDocument : MonoBehaviour
{
    public PrintRow PrintRow;

    public enum DocumentType { Email, Image, Console, Text };

    private DocumentType myType;
    private string fullText;
    int charsPerRow = 96; // The maximum number of character that can fit in one PrintRow (size 6 font "Ticketing TMP")
    int numSegments = 0;


    public void SetType(DocumentType type)
    { myType = type; }

    public void SetText(string s)
    { fullText = s; }

    public void SetHeader(Email email)
    {
        PrintRow row = Instantiate(PrintRow, this.transform);
        string subject = email.GetSubject();
        if (subject.Length > 24)
            subject = subject.Substring(0, 24);
        string headerText = subject + "...\n* * From: " + email.GetContact() +"\n* * Date: "+email.GetDate();
        row.AssignText(headerText);
        numSegments++;
    }

    public void Assemble()
    {
        SplitText();
    }

    private void SplitText()
    {
        string cleaned = fullText.Replace(Environment.NewLine, " ");
        List<string> splitString = StringExtensions.SplitInParts(cleaned, charsPerRow);

        foreach (string s in splitString)
        {
            PrintRow row = Instantiate(PrintRow, this.transform);
            row.transform.SetAsLastSibling();
            row.AssignText(s);
            numSegments++;
            // WaitForSeconds(PRINT_SPEED); --> Make into Coroutine
        }
    }
}
