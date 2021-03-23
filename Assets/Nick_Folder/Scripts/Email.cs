using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Email : MonoBehaviour
{
    RectTransform myTransform;
    EmailManager emailManager;

    //enum EmailStatus { Normal, Urgent, Flagged, Done }

    [Header("Email Information")]
    [SerializeField] Sprite portrait_sprite;
    [SerializeField] Sprite blankSquare;
    [SerializeField] string contact;
    [SerializeField] string address;
    [SerializeField] string subject;
    [SerializeField] string date;
    [SerializeField] string status;
    [SerializeField] bool isUnread;
    [SerializeField] string bodyString; 
    [SerializeField] string[] body;
    [SerializeField] bool hasAttachment;
    [SerializeField] string AttachmentPath;

    public TextMeshProUGUI tempArea;

    [Header("UI Elements")]
    [SerializeField] Image portrait_Image;
    [SerializeField] TextMeshProUGUI contactText;
    [SerializeField] TextMeshProUGUI addressText;
    [SerializeField] TextMeshProUGUI subjectText;
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] Image statusIcon;
    [SerializeField] Image background;
    [SerializeField] Button clickableButton;
    [SerializeField] Color unreadColor;
    [SerializeField] Color readColor;

    private void Awake()
    {
        emailManager = FindObjectOfType<EmailManager>();
        myTransform = this.GetComponent<RectTransform>();
        clickableButton.onClick.AddListener(() => InspectThis());
        AttachmentPath = "Emails/Attachments/AttachmentSprites/";
    }

    private void InspectThis()
    {
        emailManager.InspectEmail(this);
        if (isUnread)
        {
            isUnread = false;
            background.color = readColor;
        }
    }

    private void Start()
    {
        DisplayEmailInfo();
        background.color = unreadColor;
    }

    private void DisplayEmailInfo()
    {
        contactText.text = contact;
        addressText.text = address;
        subjectText.text = subject;
        dateText.text = date;

        // CONTACT PICTURE
        portrait_Image.sprite = portrait_sprite;
        background.sprite = blankSquare;

        SetStatusColor();
    }

    private void SetStatusColor()
    {
        switch (status)
        {
            case "1":
                statusIcon.color = emailManager.urgentStatusColor;
                break;
            case "2":
                statusIcon.color = emailManager.flaggedStatusColor;
                break;
            case "3":
                statusIcon.color = emailManager.doneStatusColor;
                break;
            default:
                statusIcon.color = emailManager.normalStatusColor;
                break;
        }
    }


    // Setters
    public void SetPortrait(Sprite s) { portrait_sprite = s; }
    public void SetContact(string s) { contact = s; }
    public void SetAddress(string s) { address = s; }
    public void SetSubject(string s) { subject = s; }
    public void SetDate(string s) { date = s; }
    public void SetStatus(string s) { status = s; }
    public void SetUnread(bool b) { isUnread = b; }
    public void SetBody(string s) { bodyString = s; }
    public void SetAttachment(string path)
    {
        if (path != "!")
        {
            hasAttachment = true;
            AttachmentPath += path;
        }
    }

    // Getters
    public Sprite GetPortrait() { return portrait_sprite; }
    public string GetContact() { return contact; }
    public string GetAddress() { return address; }
    public string GetSubject() { return subject; }
    public string GetDate() { return date; }
    public string GetStatus() { return status; }
    public string GetBody() { return bodyString; }
    public bool HasAttachment() { return hasAttachment; }
    public string GetAttachmentPath() { return AttachmentPath; }

    // Evidence Key
    private string evidenceKey;
    public void SetKey(string key) { evidenceKey = key; }
    public string GetKey() { return evidenceKey; }
}
