using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ICAControl : MonoBehaviour
{
    //This is the script for simply turning on and off ICA windows.
    public GameObject aboutUsWindow;
    public GameObject nrcBorderWindow;
    public GameObject mapWindow;
    public GameObject covidWindow;
    public GameObject joinWindow;
    public GameObject homeWindow;
    public GameObject loginWindow;

    private Printer printer;
        
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    public GameObject errorText;
    void Start()
    {
        printer = FindObjectOfType<Printer>();
    }
    
    void Update()
    {
        Debug.Log(loginEmail.text == "Email");
        Debug.Log(loginEmail.text);
        Debug.Log(loginPassword.text == "Password");
        Debug.Log(loginPassword.text);
    }

    public void aboutUS()
    {
        aboutUsWindow.SetActive(true);
        nrcBorderWindow.SetActive(false);
        mapWindow.SetActive(false);
        covidWindow.SetActive(false);
        joinWindow.SetActive(false);
        homeWindow.SetActive(false);
    }
    public void borderInfo()
    {
        aboutUsWindow.SetActive(false);
        nrcBorderWindow.SetActive(true);
        mapWindow.SetActive(false);
        covidWindow.SetActive(false);
        joinWindow.SetActive(false);
        homeWindow.SetActive(false);
    }
    public void mapInfo()
    {
        aboutUsWindow.SetActive(false);
        nrcBorderWindow.SetActive(false);
        mapWindow.SetActive(true);
        covidWindow.SetActive(false);
        joinWindow.SetActive(false);
        homeWindow.SetActive(false);
    }
    public void covidInfo()
    {
        aboutUsWindow.SetActive(false);
        nrcBorderWindow.SetActive(false);
        mapWindow.SetActive(false);
        covidWindow.SetActive(true);
        joinWindow.SetActive(false);
        homeWindow.SetActive(false);
    }
    public void joinUs()
    {
        aboutUsWindow.SetActive(false);
        nrcBorderWindow.SetActive(false);
        mapWindow.SetActive(false);
        covidWindow.SetActive(false);
        joinWindow.SetActive(true);
        homeWindow.SetActive(false);
    }
    public void goHomePage()
    {
        aboutUsWindow.SetActive(false);
        nrcBorderWindow.SetActive(false);
        mapWindow.SetActive(false);
        covidWindow.SetActive(false);
        joinWindow.SetActive(false);
        homeWindow.SetActive(true);
        loginWindow.SetActive(false);
    }

    public void logIn()
    {
        if (loginEmail.text.Equals("Email") && loginPassword.text.Equals("Password"))
        {
            
            loginWindow.SetActive(true);
            errorText.SetActive(false);
        }

        else
        {
            errorText.SetActive(true);
        }
    }
}
