using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FinancialControl : MonoBehaviour
{
    Printer printer;
    public Button IDSearchButton, EstateButton, TreasuryButton;
    public Button AccountButton, PrintIncomeButton, PrintTaxButton;

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
    private string[] currentAccount;

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

        printer = FindObjectOfType<Printer>();
        PrintIncomeButton.onClick.AddListener(() => PrintStatement(true));
        PrintTaxButton.onClick.AddListener(() => PrintStatement(false));
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
            if (input.ToLower() == arr[0].ToLower())
            {
                currentAccount = arr;
                return arr;
            }
            else
                continue;
        }
        // If no results found, return this...
        currentAccount = null;
        return new string[] { "No Results Found." };
    }

    private void DisplaySearchResults(string[] arr)
    {
        if (arr.Length > 1) // IF VALID
        {
            resultsName.text = "Name:      <color=#012456>" + arr[0]+ "</color>";
            resultsType.text = "Type:      <color=#012456>" + arr[1] + "</color>";
            resultsAddress.text = "Address:   <color=#012456>" + arr[2] + "</color>";
            resultsIncome.text = "Income:    <color=#012456>" + arr[3] + "</color>";
            resultsTaxes.text = "Taxes:     <color=#012456>" + arr[4] + "</color>";
            resultsTaxStatus.text = "Payment:   <color=#012456>" + arr[5] + "</color>";
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

    private void PrintStatement(bool isIncome)
    {
        if (currentAccount != null)
        {
            if (isIncome)
            {
                printer.Print(currentAccount[0], "Yearly Income: "+currentAccount[3], "Current Balance: "+currentAccount[8], int.Parse(currentAccount[4]), "INCOME");
                Debug.Log(int.Parse(currentAccount[4]));
            }
            else
            {
                printer.Print(currentAccount[0], "Taxes Owed: "+currentAccount[5], "Status: "+currentAccount[7], int.Parse(currentAccount[6]), "TAX");
            }
        }
        else
        {
            // No Current Account
            Debug.Log("No current account to print");
        }
    }
}
