using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailManager : MonoBehaviour
{
    [SerializeField] RectTransform TopOfInbox;
    [SerializeField] int emailHeight = 50;

    [SerializeField] List<Email> Emails = new List<Email>();

    [Space(10)]
    [SerializeField] Image BodyPanel;
    [SerializeField] Color emptyColor;
    [SerializeField] Color normalColor;
    [SerializeField] Image portraitIMG;
    [SerializeField] TextMeshProUGUI contactTMP;
    [SerializeField] TextMeshProUGUI addressTMP;
    [SerializeField] TextMeshProUGUI dateTMP;
    [SerializeField] TextMeshProUGUI subjectTMP;
    [SerializeField] TextMeshProUGUI bodyTMP;

    Email ActiveEmail;

    private void Awake()
    {
        OrganizeInbox();
    }

    private void Start()
    {
        foreach (Email e in Emails)
        {
            e.SetY();
        }
    }

    void OrganizeInbox()
    {
        for (int i = 0; i < Emails.Count; i++)
        {
            Emails[i].SetNumber(i);
        }
    }

    public void InspectEmail(Email email)
    {
        if (!BodyPanel.IsActive())
            BodyPanel.gameObject.SetActive(true);

        ActiveEmail = email;

        //string fullEmail = email.ReturnBody();

        //string[] splitLines = Regex.Split(fullEmail, "\r\n|\r|\n");

        portraitIMG.sprite = email.GetPortrait();
        contactTMP.text = email.GetContact();
        addressTMP.text = email.GetAddress();
        subjectTMP.text = email.GetSubject();
        dateTMP.text = email.GetDate();
        bodyTMP.text = email.GetBody();



    }
}
