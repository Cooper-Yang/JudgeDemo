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
    public int pageNum;
    private Color temp_c;
    void Start()
    {
        pageNum = 0;
        LeftList.Insert(0,"");
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
    }

    public void PageDown()
    {
        if(pageNum < LeftList.Count - 1)
            pageNum += 1;
    }

    public void hhh()
    {
        Debug.Log("Click");
    }
}
