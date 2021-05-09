using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FinancialControl : MonoBehaviour
{
    Printer printer;
    public Button IDSearchButton, TreasuryButton;
    public Button AccountButton, PrintIncomeButton, PrintTaxButton;

    public GameObject[] Panels = new GameObject[2];

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
        TreasuryButton.onClick.AddListener(() => ChangePanel(1));
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
        //string[] lines = personnelCSV.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        string[] lines = personnelCSV.text.Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
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
            string search = input.ToLower();
            if (arr[0].ToLower().Contains(search) && search.Length > 2)
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
            resultsTaxes.text = "Taxes:     <color=#012456>" + arr[5] + "</color>";
            resultsTaxStatus.text = "Payment:   <color=#012456>" + arr[7] + "</color>";
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
            string[] blank = new string[1] { "No Account Selected" };
            DisplaySearchResults(blank);
        }
    }

    public void PrintCommitteeReport(int i)
    {
        int[] payments;
        switch (i)
        {
            case 1:
                payments = new int[8] { 2100, -1400, -950, 1675, 990, 1400, -1150, 1025 };
                printer.Print(i, "Committee of Justice", payments);
                break;
            case 2:
                payments = new int[6] { 1200, 1400, -5000, 850, -1065, -900 };
                printer.Print(i, "Committee of Harmony", payments);
                break;
            case 3:
                payments = new int[5] { 850, 1250, 1600, -1325, -1000 };
                printer.Print(i, "Committee of Prosperity", payments);
                break;
            default:
                break;
        }
    }
}
