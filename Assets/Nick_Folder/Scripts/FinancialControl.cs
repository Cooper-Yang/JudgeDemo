using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FinancialControl : MonoBehaviour
{
    public Button IDSearchButton, EstateButton, TreasuryButton;
    public GameObject[] Panels = new GameObject[3];

    [Header("ID Search")]
    public TMP_InputField inputField;
    public Button SearchButton;
    public TextAsset personnelCSV;
    public TextMeshProUGUI resultsName;
    public TextMeshProUGUI resultsType;
    public TextMeshProUGUI resultsAddress;
    public TextMeshProUGUI resultsIncome;
    public TextMeshProUGUI resultsTaxes;
    public TextMeshProUGUI resultsTaxStatus;

    private List<string[]> listIDs;

    [Header("Estate Values")]
    public float e;

    [Header("Treasury")]
    public float t;

    private void Awake()
    {
        IDSearchButton.onClick.AddListener(() => ChangePanel(0));
        EstateButton.onClick.AddListener(() => ChangePanel(1));
        TreasuryButton.onClick.AddListener(() => ChangePanel(2));
        ChangePanel(0);
        SearchButton.onClick.AddListener(() => DisplaySearchResults(SearchID()));
        listIDs = SplitCSV();
    }

    private void ChangePanel(int count)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i == count)
            {
                if(!Panels[i].activeSelf)
                    Panels[i].SetActive(true);
            }
            else
            {
                if (Panels[i].activeSelf)
                    Panels[i].SetActive(false);
            }
        }
    }

    private List<string[]> SplitCSV()
    {
        // Called on Awake -- new List of string[]
        List<string[]> fullList = new List<string[]>();
        // Split total text file by NewLine -- string[] of lines 
        string[] lines = personnelCSV.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        // Split each line into string[] seperated by ',' -- add to List
        foreach (string line in lines)
        {
            string[] lineData = line.Split(',');
            fullList.Add(lineData);
        }
        return fullList;
    }

    private string[] SearchID()
    {
        // Called on Search Button Press
        string input = inputField.text.ToString();
        // Search through the listIDs using the Input word
        foreach (string[] arr in listIDs)
        {
            if (input == arr[0])
            {
                return arr;
            }
            else
                continue;
        }
        // If no results found, return this...
        return new string[] { "No Results Found." };
    }

    private void DisplaySearchResults(string[] arr)
    {
        if (arr.Length > 1) // IF VALID
        {
            resultsName.text = "Name:      " + arr[0];
            resultsType.text = "Type:      " + arr[1];
            resultsAddress.text = "Address:   " + arr[2];
            resultsIncome.text ="Income:    " + arr[3];
            resultsTaxes.text = "Taxes:     " + arr[4];
            resultsTaxStatus.text = "Payment:   " + arr[5];
        }
        else
        {
            resultsName.text = arr[0];
            resultsType.text = "";
            resultsAddress.text = "";
            resultsIncome.text = "";
            resultsTaxes.text = "";
            resultsTaxStatus.text = "";
        }
    }
}
