using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ManualController : MonoBehaviour
{
    public TextMeshProUGUI leftPg;
    public TextMeshProUGUI rightPg;
    public TextMeshProUGUI title;
    public TextMeshProUGUI catalog;
    public Button index_1;
    public Button index_2;
    public Button index_3;
    public Button index_4;
    public Button pgup;
    public Button pgdw;
    public Button back;
    public ClueText clueText;
    public List<string> LeftList;
    public int pageNum = 0;
    private Color temp_c;
    void Start()
    {
        pageNum = 0;
        LeftList.Insert(0,"");
        LeftList.Add("INTRODUCTION:\n\nComrades:\nThis book is the 86th edition of the Office Manual of the Committee of the Justice. " +
            "If there is a newer version, please refer to the latest version.\n\nThe purpose of this manual is to smooth your entry path" +
            " and help you when working for our country.\n\nThis book is divided into three parts: routines, computer system guide, and " +
            "submission board guide.\n\nIf you have any special questions, pleases consult your direct learder.");
        LeftList.Add("ROUTINES:\n\nCongratulations on becoming the backbone of our party. The following will explain the specific instructions " +
            "for daily work.\n\nYou are a junior judge. Your task is to find the evidence of the case and submit it th the central government " +
            "for the next trial.\n\nFirst of all, your first thing is to check your computer's mail. Your superior will assign you tasks.\n\nSecond, " +
            "you need to find the corresponding evidence according to the task instrctions. You need to screen different applications on your computer " +
            "to find evidence and print it out. The printed material is on the evidence board behind the computer.(For specific computer applications, please refer to the computer's guide)"+
            "\n\nFinally, after you feel that you have found all the evidence, drag the required evidence from the evidence board to the submission board and submit it. (For specific board operations, please refer to the board's guide");
        
        LeftList.Add("COMPUTER SYSTEM GUIDE:\n\nThe following is a tutorial for dummies of computer systems.\n\nThis computer system is a high-tech " +
            "system developed by our party that surpasses the West for 76 years. Now it contains 5 applications.\n\nMail System:\nCan be used to receive " +
            "mail. You can click the icon in the upper right corner of the email and then print it. Click 'Log Out' to switch users. Emails sometimes " +
            "contain attachments.\n\nConsole:\nAt present, it is mainly used to generate a password (Skeleton Key) for stealing another person's mailbox. " +
            "Enter help in the console to get other instructions.\n\n"+"Financial Database:\nYou can view 1. The financial statements of the committees 2. The income and taxation of citizens.\n\nICA " +
            "(Immigration & Checkpoints Authority):\nYou can get the records of each customs. At the same time you can view the geographic information of " +
            "our great motherland.\n\nLaw Enforcement Database:\nYou can view the crime incidents that occurred in recent days, and print specifice crime evidence.");
        LeftList.Add("SUBMISSION BOARD GUIDE:\n\nWhenever you finish printing information or evidence, you can sort it out on the board on the right and " +
            "drag the evidence you need to the board on the left.\n\nOn the Report board on the left, you can select the case you want to work on (click " +
            "the rectangular bbutton on the left of the submit case to select).\n\nClick the submit button to submit. Please consider carefully before submitting " +
            "something, there is no turning back.");
        pgup.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }

    void Update()
    {
        if(pageNum == 0)
        {
            title.gameObject.SetActive(true);
            catalog.gameObject.SetActive(true);
            index_1.gameObject.SetActive(true);
            index_2.gameObject.SetActive(true);
            index_3.gameObject.SetActive(true);
            index_4.gameObject.SetActive(true);
            pgup.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
            pgdw.gameObject.SetActive(true);
        }
        else
        {
            title.gameObject.SetActive(false);
            catalog.gameObject.SetActive(false);
            index_1.gameObject.SetActive(false);
            index_2.gameObject.SetActive(false);
            index_3.gameObject.SetActive(false);
            index_4.gameObject.SetActive(false);
            pgup.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
            if (pageNum == LeftList.Count - 1)
                pgdw.gameObject.SetActive(false);
            else
                pgdw.gameObject.SetActive(true);
        }
        if(pageNum < LeftList.Count)
            leftPg.text = LeftList[pageNum];
    }

    public void BackToCata()
    {
        pageNum = 0;
        clueText = back.GetComponentInChildren<ClueText>();
        clueText.changeColor();
    }

    public void PageUp()
    {
        if(pageNum > 0)
            pageNum -= 1;
        SoundMan.me.BookTurnSound();
    }

    public void PageDown()
    {
        if(pageNum < LeftList.Count - 1)
            pageNum += 1;
        SoundMan.me.BookTurnSound();
    }

    public void Introduction()
    {
        pageNum = 1;
        clueText = index_1.GetComponentInChildren<ClueText>();
        clueText.changeColor();
    }
    public void Routines()
    {
        pageNum = 2;
        clueText = index_2.GetComponentInChildren<ClueText>();
        clueText.changeColor();
    }
    public void Computer()
    {
        pageNum = 3;
        clueText = index_3.GetComponentInChildren<ClueText>();
        clueText.changeColor();
    }
    public void Board()
    {
        pageNum = 4;
        clueText = index_4.GetComponentInChildren<ClueText>();
        clueText.changeColor();
    }
}
