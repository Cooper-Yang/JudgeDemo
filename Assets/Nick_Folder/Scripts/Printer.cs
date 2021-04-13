﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    public GameObject PrinterSceneObject;
    public PrintDocument PrintDocumentPrefab;
    public GameObject canvas;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Print(TempEmail);
            //Print(PrintDocument.DocumentType.Text, "If you prefer to not use an extension method remove the this keyword and rename class and method appropriately. I think this particular method is pretty generic and not specific to say order numbers) and a good candidate for an extension method. Putting the class in a separate namespace enables the developer to decide if he wants to enable the extension methods just as adding using System.Linq adds a bunch of extension methods to IEnumerable<T>");
        }
    }

    public void Print(Email email)
    {
        SoundMan.me.PrintingSound();
        PrintDocument Doc = Instantiate(PrintDocumentPrefab, PrinterSceneObject.transform.position, Quaternion.identity, canvas.transform);
        //Doc.SetType()
        Doc.SetHeader(email);
        Doc.SetText(email.GetBody());
        Doc.SetKey(email.GetKey());
        Doc.Assemble();
    }

    public void Print(CrimeBlock block)
    {
        SoundMan.me.PrintingSound();
        PrintDocument Doc = Instantiate(PrintDocumentPrefab, PrinterSceneObject.transform.position, Quaternion.identity, canvas.transform);
        //Doc.SetType()
        Doc.SetHeader(block.date.text+" "+block.location.text);
        Doc.SetText(block.content1.text+" "+block.content2.text);
        Doc.SetKey(block.key);
        Doc.Assemble();
    }

    public void Print(string[] lines)
    {
        SoundMan.me.PrintingSound();
        string combined = string.Join("\n", lines);
        PrintDocument Doc = Instantiate(PrintDocumentPrefab, PrinterSceneObject.transform.position, Quaternion.identity, canvas.transform);
        Doc.SetHeader(":CONSOLE OUTPUT:");
        Doc.SetText(combined);
        Doc.SetKey("console");
        Doc.Assemble();

    }

    public void Print(Sprite picture, string name)
    {
        SoundMan.me.PrintingSound();
        PrintDocument Doc = Instantiate(PrintDocumentPrefab, PrinterSceneObject.transform.position, Quaternion.identity, canvas.transform);
        Doc.SetText("Photo: "+name);
        Doc.SetImage(picture);
        Doc.SetKey(name);
        Doc.Assemble();
    }

}

static class StringExtensions
{
    public static List<string> SplitInParts(string s, int partLength)
    {
        if (s == null || partLength <= 0)
        { return new List<string> { "Split String Error" }; }
        else
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < s.Length; i += partLength)
            {
                string s1 = s.Substring(i, Mathf.Min(partLength, s.Length - i));
                if (s1[s1.Length-1] != ' ')
                    s1 += "-";
                lines.Add(s1);
            }

            return lines;
        }
    }

}
