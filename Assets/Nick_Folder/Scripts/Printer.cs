using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    public GameObject PrinterSceneObject;
    public PrintDocument PrintDocumentPrefab;
    public GameObject WorldCanvas;

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

    // UNUSED
    public void Print(PrintDocument.DocumentType docType, string text)
    {
        PrintDocument Doc = Instantiate(PrintDocumentPrefab, PrinterSceneObject.transform.position, Quaternion.identity, WorldCanvas.transform);
        Doc.SetType(docType);

        switch (docType)
        {
            case PrintDocument.DocumentType.Email:
                break;

            case PrintDocument.DocumentType.Image:
                break;

            case PrintDocument.DocumentType.Text:
                Doc.SetText(text);
                break;

            default:
                break;
        }

        Doc.Assemble();
    }

    public void Print(Email email)
    {
        PrintDocument Doc = Instantiate(PrintDocumentPrefab, PrinterSceneObject.transform.position, Quaternion.identity, WorldCanvas.transform);
        //Doc.SetType()
        Doc.SetHeader(email);
        Doc.SetText(email.GetBody());
        Doc.Assemble();
    }

    public void Print(TextAsset textFile)
    {

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
