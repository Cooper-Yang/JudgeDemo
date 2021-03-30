using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrintDocument : MonoBehaviour
{
    public GameObject LayoutGroup;
    public PrintRow PrintRow;
    public TextMeshProUGUI thisText;
    public Image picture;
    public Image backgroundImage;

    /*public enum DocumentType { Email, Image, Console, Text };
    private DocumentType myType;
    public void SetType(DocumentType type)
    { myType = type; }*/

    private string fullText;
    [SerializeField] private string evidenceKey;
    int charsPerRow = 96; // The maximum number of character that can fit in one PrintRow (size 6 font "Ticketing TMP")
    int numSegments = 0;
    bool isImage = false;



    public void SetText(string s)
    { fullText = s; }

    public void SetKey(string k)
    { evidenceKey = k; }

    public string GetKey()
    { return evidenceKey; }

    public void SetHeader(Email email)
    {
        PrintRow row = Instantiate(PrintRow, LayoutGroup.transform);
        string subject = email.GetSubject();
        if (subject.Length > 24)
            subject = subject.Substring(0, 24);
        string headerText = subject + "...\n* * From: " + email.GetContact() +"\n* * Date: "+email.GetDate() + "\n* * * * * * * * * * * *";
        row.AssignText(headerText);
        row.transform.SetAsFirstSibling();
        numSegments++;
        isImage = false;
    }

    public void SetHeader(string String)
    {
        PrintRow row = Instantiate(PrintRow, LayoutGroup.transform);
        string headerText = String + "\n* * * * * * * * * * * *";
        row.AssignText(headerText);
        row.transform.SetAsFirstSibling();
        numSegments++;
        isImage = false;
    }

    public void SetImage(Sprite image)
    {
        picture.sprite = image;
        isImage = true;
    }

    public void Assemble()
    {
        //SplitText();
        thisText.text = fullText;

        if (isImage)
        {
            picture.enabled = true;
            backgroundImage.enabled = true;
        }
        else
        {
            picture.enabled = false;
            backgroundImage.enabled = false;
        }
    }

    private void SplitText()
    {
        string cleaned = fullText.Replace(Environment.NewLine, " ");
        List<string> splitString = StringExtensions.SplitInParts(cleaned, charsPerRow);

        foreach (string s in splitString)
        {
            PrintRow row = Instantiate(PrintRow, LayoutGroup.transform);
            row.transform.SetAsLastSibling();
            row.AssignText(s);
            numSegments++;
            // WaitForSeconds(PRINT_SPEED); --> Make into Coroutine
        }
    }
}
