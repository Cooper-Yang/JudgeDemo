using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Email : MonoBehaviour
{
    enum EmailStatus { Normal, New, Urgent, Flagged, Done }
    RectTransform myTransform;

    [SerializeField] int EmailNumber;
    [SerializeField] bool isSelected;

    [Space(10)]

    [SerializeField] TextAsset EmailTxt;

    [SerializeField] Sprite portrait_sprite;
    [SerializeField] string contact;
    [SerializeField] string address;
    [SerializeField] string subject;
    [SerializeField] string date;
    [SerializeField] EmailStatus status;

    [Space(10)]

    [SerializeField] Image portrait_Image;
    [SerializeField] TextMeshProUGUI contactText;
    [SerializeField] TextMeshProUGUI addressText;
    [SerializeField] TextMeshProUGUI subjectText;
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] Image statusIcon;


    // in EMAIL MANAGER ***
    [Space(10)]
    public Color normalStatus;
    public Color newStatus;
    public Color urgentStatus;
    public Color flaggedStatus;
    public Color doneStatus;



    //Image thisEmailItem;

    private void Awake()
    {
        //thisEmailItem = this.GetComponent<Image>();
        myTransform = this.GetComponent<RectTransform>();

        portrait_Image.sprite = portrait_sprite;
        contactText.text = contact;
        addressText.text = address;
        subjectText.text = subject;
        dateText.text = date;
        SetStatusColor();
    }

    private void SetStatusColor()
    {
        switch (status.ToString())
        {
            case "Normal":
                statusIcon.color = normalStatus;
                break;
            case "New":
                statusIcon.color = newStatus;
                break;
            case "Urgent":
                statusIcon.color = urgentStatus;
                break;
            case "Flagged":
                statusIcon.color = flaggedStatus;
                break;
            case "Done":
                statusIcon.color = doneStatus;
                break;
        }
    }

    public void SetNumber(int x)
    {
        EmailNumber = x;
    }

    public void SetY()
    {
        int tempY = -50 * EmailNumber;
        myTransform.localPosition = new Vector3(0, tempY, 0);
    }

    public string GetContact() { return contact; }
    public string GetAddress() { return address; }
    public string GetSubject() { return subject; }
    public string GetDate() { return date; }
    public Sprite GetPortrait() { return portrait_sprite; }
    public string GetBody()
    {
        if (EmailTxt != null)
            return EmailTxt.text;
        else
            return "- - -";
    }


}
