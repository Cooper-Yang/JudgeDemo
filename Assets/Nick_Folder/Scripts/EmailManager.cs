using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EmailManager : MonoBehaviour
{
    [SerializeField] RectTransform TopOfInbox;
    [SerializeField] int emailHeight = 50;
    [SerializeField] int inboxCapacity = 20;
    [SerializeField] int emailCount = 0;

    // LIST OF ALL EMAIL FILES - unique to each user, referenced separetly
    [SerializeField] Email EmailPrefab;
    [SerializeField] List<TextAsset> PlayerAllFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> BossAllFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> HitchcockAllFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> XiaoWangAllFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> LeeAllFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> FangZhouAllFiles = new List<TextAsset>();

    // INBOXES:
    [SerializeField] List<TextAsset> PlayerInboxFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> BossInboxFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> HitchcockInboxFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> XiaoWangInboxFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> LeeInboxFiles = new List<TextAsset>();
    [SerializeField] List<TextAsset> FangZhouInboxFiles = new List<TextAsset>();

    // Inboxes - Read/Unread;
    [SerializeField] List<bool> PlayerReadList = new List<bool>();
    [SerializeField] List<bool> BossReadList = new List<bool>();
    [SerializeField] List<bool> XiaoWangReadList = new List<bool>();
    [SerializeField] List<bool> LeeReadList = new List<bool>();
    [SerializeField] List<bool> FangZhouReadList = new List<bool>();


    [SerializeField] List<Email> InboxEmails = new List<Email>();
    [SerializeField] Email ActiveEmail;
    // Sort Emails by Contact OR Date OR Case OR Flagged -- Maybe dropdown?

    [Space(10)] // ~~ Current Open Email ~~
    [SerializeField] Image BodyPanel;
    [SerializeField] Color emptyColor;
    [SerializeField] Color normalColor;
    [SerializeField] Image portraitIMG;
    [SerializeField] TextMeshProUGUI contactTMP;
    [SerializeField] TextMeshProUGUI addressTMP;
    [SerializeField] TextMeshProUGUI dateTMP;
    [SerializeField] TextMeshProUGUI subjectTMP;
    [SerializeField] TextMeshProUGUI statusTMP;
    [SerializeField] TextMeshProUGUI bodyTMP;


    [Space(10)] // ~~ Email Application Window ~~
    [SerializeField] GameObject MainPanel;
    [SerializeField] TextMeshProUGUI UsernameText;
    [SerializeField] GameObject LogOutButton;
    [SerializeField] GameObject LoginPanel;
    [SerializeField] GameObject LogInButton;
    [SerializeField] GameObject OpenAttachmentButton;
    [SerializeField] Image loadingBar;
    [SerializeField] TextMeshProUGUI loginResultText;
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] string[] usernameArray;
    [SerializeField] string[] passwordArray;
    [SerializeField] Sprite[] loadingSprites;
    
    [Space(10)] // ~~ Attachments ~~
    [SerializeField] FileWindow NewWindowPrefab;
    [SerializeField] GameObject ComputerPanel;


    public Color normalStatusColor;
    public Color newStatusColor;
    public Color urgentStatusColor;
    public Color flaggedStatusColor;
    public Color doneStatusColor;

    private string skeletonKey;
    public void SetSkeletonKey(string s)
    { skeletonKey = s; }

    private enum EmailUsers { Yao, Boss, Hitchcock, XiaoWang, Lee, FangZhou };
    [SerializeField] private EmailUsers currentUser;

    private void Awake()
    {
        PlayerReadList = InitReadLists(PlayerInboxFiles);
        BossReadList = InitReadLists(BossInboxFiles);
        XiaoWangReadList = InitReadLists(XiaoWangInboxFiles);
        LeeReadList = InitReadLists(LeeInboxFiles);
        FangZhouReadList = InitReadLists(FangZhouInboxFiles);
        InitInbox(currentUser.ToString());
    }

    public TextAsset test;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(test != null)
                CreateNewEmailToPlayer(test);
        }
    }

    private void InitInbox(string username) // USER parameter
    {
        UsernameText.text = username.ToString();
        List<TextAsset> userInbox;
        List<bool> isRead = new List<bool>();
        switch (username)
        {
            case "Yao":
                userInbox = PlayerInboxFiles;
                isRead = PlayerReadList;
                break;
            case "Boss":
                userInbox = BossInboxFiles;
                isRead = BossReadList;
                break;
            case "Hitchcock":
                userInbox = HitchcockInboxFiles;
                break;
            case "XiaoWang":
                userInbox = XiaoWangInboxFiles;
                isRead = XiaoWangReadList;
                break;
            case "Lee":
                userInbox = LeeInboxFiles;
                isRead = LeeReadList;
                break;
            case "FangZhou":
                userInbox = FangZhouInboxFiles;
                isRead = FangZhouReadList;
                break;
            default:
                userInbox = new List<TextAsset>();
                isRead = PlayerReadList;
                break;
        }

        // Create new Email object for each Email File in the inbox
        int i = 0;
        foreach (TextAsset f in userInbox)
        {
            /*bool hasbeenRead = false;
            int spot = userInbox.IndexOf(f);
            if (isRead != null && spot > -1)
            {
                hasbeenRead = isRead[i];
            }
            Debug.Log(hasbeenRead);*/


            //InboxEmails.Add(CreateEmailFromFile(f, hasbeenRead));
            InboxEmails.Add(CreateEmailFromFile(f, isRead[i]));
            emailCount++;
            i++;
        }
    }

    private List<bool> InitReadLists(List<TextAsset> files)
    {
        List<bool> bools = new List<bool>();
        for (int i = 0; i < files.Count; i++)
        {
            bools.Add(false);
        }

        return bools;
    }

    private Email CreateEmailFromFile(TextAsset file, bool isRead)
    {
        string fullEmail = file.text;

        string[] lines;
        if (fullEmail.Contains("#"))
        {
            lines = fullEmail.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            lines = null;
        }

        Email email = Instantiate(EmailPrefab, TopOfInbox);
        email.transform.SetAsFirstSibling();

        email.SetContact(lines[1]);
        email.SetAddress(lines[3]);
        email.SetSubject(lines[5]);
        email.SetDate(lines[7]);
        email.SetStatus(lines[9]);
        email.SetAttachment(lines[11]);
        email.SetKey(lines[13]);
        email.SetBody(lines[15]);
        email.SetUnread(!isRead);
        email.SetMyFile(file);

        return email;
    }

    public void InspectEmail(Email email)
    {
        if (!BodyPanel.IsActive())
            BodyPanel.gameObject.SetActive(true);

        ActiveEmail = email;
        if (ActiveEmail.HasAttachment())
            OpenAttachmentButton.SetActive(true);
        else
            OpenAttachmentButton.SetActive(false);

        //string fullEmail = email.ReturnBody();

        //string[] splitLines = Regex.Split(fullEmail, "\r\n|\r|\n");


        portraitIMG.sprite = email.GetPortrait();
        contactTMP.text = email.GetContact();
        addressTMP.text = email.GetAddress();
        subjectTMP.text = email.GetSubject();
        dateTMP.text = email.GetDate();

        string statusN = email.GetStatus();
        string status;
        switch (statusN)
        {
            case "1":
                statusTMP.color = urgentStatusColor;
                status = "URGENT:";
                subjectTMP.margin = new Vector4(40, 0, 0, 0);
                break;
            case "2":
                statusTMP.color = flaggedStatusColor;
                status = "Flagged:";
                subjectTMP.margin = new Vector4(40, 0, 0, 0);
                break;
            case "3":
                statusTMP.color = doneStatusColor;
                subjectTMP.margin = new Vector4(45, 0, 0, 0);
                status = "Complete:";
                break;
            default:
                statusTMP.color = normalStatusColor;
                status = "";
                subjectTMP.margin = new Vector4(0, 0, 0, 0);
                break;
        }
        statusTMP.text = status;

        bodyTMP.text = email.GetBody();

        // determine current user
        List<TextAsset> thisList = new List<TextAsset>();
        int spotInList = -1;
        switch (currentUser)
        {
            case EmailUsers.Yao:
                spotInList = PlayerInboxFiles.IndexOf(email.GetMyFile());
                PlayerReadList = UpdateReadList(spotInList, PlayerReadList);
                break;
            case EmailUsers.Boss:
                spotInList = BossInboxFiles.IndexOf(email.GetMyFile());
                PlayerReadList = UpdateReadList(spotInList, BossReadList);
                break;
            case EmailUsers.XiaoWang:
                spotInList = XiaoWangInboxFiles.IndexOf(email.GetMyFile());
                XiaoWangReadList = UpdateReadList(spotInList, XiaoWangReadList);
                break;
            case EmailUsers.Lee:
                spotInList = LeeInboxFiles.IndexOf(email.GetMyFile());
                LeeReadList = UpdateReadList(spotInList, LeeReadList);
                break;
            case EmailUsers.FangZhou:
                spotInList = FangZhouInboxFiles.IndexOf(email.GetMyFile());
                FangZhouReadList = UpdateReadList(spotInList, FangZhouReadList);
                break;
            default:
                break;

        }
    }

    private List<bool> UpdateReadList(int spot, List<bool> readList)
    {
        readList[spot] = true;
        return readList;
    }

    public void CreateNewEmail()
    {      
        List<TextAsset> UserFiles;
        List<TextAsset> UserInbox;

        string username = currentUser.ToString();
        switch (username)
        {
            case "Player":
                UserFiles = PlayerAllFiles;
                UserInbox = PlayerInboxFiles;
                break;
            case "Boss":
                UserFiles = BossAllFiles;
                UserInbox = BossInboxFiles;
                break;
            case "Hitchcock":
                UserFiles = HitchcockAllFiles;
                UserInbox = HitchcockInboxFiles;
                break;
            case "XiaoWang":
                UserFiles = XiaoWangAllFiles;
                UserInbox = XiaoWangInboxFiles;
                break;
            case "Lee":
                UserFiles = LeeAllFiles;
                UserInbox = LeeInboxFiles;
                break;
            case "FangZhou":
                UserFiles = FangZhouAllFiles;
                UserInbox = FangZhouInboxFiles;
                break;
            default:
                UserFiles = new List<TextAsset>();
                UserInbox = new List<TextAsset>();
                break;
        }

        if (emailCount < UserFiles.Count) // make sure there is a new email file available.
        {
            if (emailCount < inboxCapacity*10) // check that there is room in the inbox
            {
                UserInbox.Add(UserFiles[emailCount]);
                Email emailTemp = CreateEmailFromFile(UserFiles[emailCount], false);
                InboxEmails.Add(emailTemp);
                //OrganizeInbox();
                emailCount++;
            }
            else
            {
                // IF THE INBOX IS FULL:
                Debug.Log("Inbox Full!");
            }
        }
        else
        {
            // NO MORE EMAIL FILES
            Debug.Log("No more email files !");
        }
    }

    // Call from Evidence Manager
    // Provide with TextAsset (.txt) to create new email object in player's inbox
    // Can modify this method in the future to deliver emails to different user inboxes.
    public void CreateNewEmailToPlayer(TextAsset textFile)
    {
        SoundMan.me.EmailReceivedSound();
        PlayerInboxFiles.Add(textFile);
        if (currentUser == EmailUsers.Yao)
        {
            Email emailNew = CreateEmailFromFile(textFile, false);
            InboxEmails.Add(emailNew);
           
        }
        PlayerReadList.Add(false);
        //emailCount++;
    }


    public void LogOut()
    {
        ClearInbox();
        emailCount = 0;
        LogOutButton.SetActive(false);
        MainPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }

    private void ClearInbox()
    {
        foreach (Email e in InboxEmails)
        {
            Destroy(e.gameObject);
        }
        InboxEmails.Clear();
    }

    public void LogInAttempt()
    {
        string username = usernameInput.text.ToString();
        string password = passwordInput.text.ToString();

        bool success = false;
        for (int i = 0; i < usernameArray.Length; i++)
        {
            if (username == usernameArray[i] && (password == passwordArray[i] || password == skeletonKey))
            {
                success = true;
                if (password == skeletonKey)
                    skeletonKey = "!!!";
                break;
            }
        }
        StartCoroutine(LoginProcedure(success, username));
    }

    private IEnumerator LoginProcedure(bool success, string name)
    {
        LogInButton.SetActive(false);
        float delay = 0.5f;

        loadingBar.sprite = loadingSprites[0];
        loadingBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        for (int i = 1; i < 3; i++)
        {
            loadingBar.sprite = loadingSprites[i];
            yield return new WaitForSeconds(delay);
        }

        if (success)
        {
            loadingBar.sprite = loadingSprites[3];
            loginResultText.text = "Welcome Back, " + name;
            loginResultText.color = new Color(0.164f, 0.549f, 0.392f);
            loginResultText.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay * 3);
            LogInAsUser(name);
        }
        else
        {
            loadingBar.sprite = loadingSprites[4];
            loginResultText.text = "Invalid Credentials";
            loginResultText.color = new Color(0.674f, 0.196f, 0.196f);
            loginResultText.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay * 3);
            loadingBar.gameObject.SetActive(false);
            loginResultText.gameObject.SetActive(false);
            LogInButton.SetActive(true);
        }
    }

    private void LogInAsUser(string username)
    {
        loadingBar.gameObject.SetActive(false);
        loginResultText.gameObject.SetActive(false);
        LogInButton.SetActive(true);

        LoginPanel.SetActive(false);
        MainPanel.SetActive(true);
        BodyPanel.gameObject.SetActive(false);
        LogOutButton.SetActive(true);

        switch (username)
        {
            case "yaobang@gov.ch":
                currentUser = EmailUsers.Yao;
                InitInbox("Yao");
                break;
            case "wusun@gov.ch":
                currentUser = EmailUsers.Boss;
                InitInbox("Boss");
                break;
            case "Hitchcock":
                currentUser = EmailUsers.Hitchcock;
                InitInbox("Hitchcock");
                break;
            case "xwang8@gov.ch":
                currentUser = EmailUsers.XiaoWang;
                InitInbox("XiaoWang");
                break;
            case "sclee@gov.ch":
                currentUser = EmailUsers.Lee;
                InitInbox("Lee");
                break;
            case "fangzhou@web.com":
                currentUser = EmailUsers.FangZhou;
                InitInbox("FangZhou");
                break;
        }
    }

    public void OpenAttachment()
    {
        if (ActiveEmail.HasAttachment())
        {
            Debug.Log("OPEN ATTACHMENT");

            FileWindow newWindow = Instantiate(NewWindowPrefab, ComputerPanel.transform);
            string path = ActiveEmail.GetAttachmentPath();
            newWindow.ReadFile(path);

            newWindow.transform.SetAsLastSibling();
        }
        else
        {
            Debug.Log("No Attachment Found...");
        }
    }

    public void PrintEmail()
    {
        if (ActiveEmail != null)
        {
            Printer printerControl = this.GetComponent<Printer>();
            printerControl.Print(ActiveEmail);
        }
    }
}
