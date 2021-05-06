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
    public GameObject requestWindow;
    public GameObject provinceWindow;
    public GameObject cityWindow;

    private Printer printer;
        
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    public GameObject errorText;
    public GameObject errorRequestText;

    public TMP_Dropdown outposts;
    public TMP_Dropdown times;
    
    public FileWindow NewWindowPrefab;
    public GameObject ComputerPanel;

    public GameObject provOne;
    public GameObject provTwo;
    public GameObject provThree;
    public GameObject provFour;
    public GameObject provFive;
    public GameObject provSix;
    public GameObject turn2Gether;
    public GameObject back2Prov;
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

    #region HomePageButtons

    public void aboutUS()
        {
            aboutUsWindow.SetActive(true);
            nrcBorderWindow.SetActive(false);
            mapWindow.SetActive(false);
            covidWindow.SetActive(false);
            joinWindow.SetActive(false);
            homeWindow.SetActive(false);
            requestWindow.SetActive(false);
            loginWindow.SetActive(false);
            provinceWindow.SetActive(false);
            cityWindow.SetActive(false);
        }
        public void borderInfo()
        {
            aboutUsWindow.SetActive(false);
            nrcBorderWindow.SetActive(true);
            mapWindow.SetActive(false);
            covidWindow.SetActive(false);
            joinWindow.SetActive(false);
            homeWindow.SetActive(false);
            requestWindow.SetActive(false);
            loginWindow.SetActive(false);
            provinceWindow.SetActive(false);
            cityWindow.SetActive(false);
        }
        public void homePage()
        {
            aboutUsWindow.SetActive(false);
            nrcBorderWindow.SetActive(false);
            mapWindow.SetActive(false);
            covidWindow.SetActive(false);
            joinWindow.SetActive(false);
            homeWindow.SetActive(true);
            requestWindow.SetActive(false);
            loginWindow.SetActive(false);
            provinceWindow.SetActive(false);
            cityWindow.SetActive(false);
        }
        public void covidInfo()
        {
            aboutUsWindow.SetActive(false);
            nrcBorderWindow.SetActive(false);
            mapWindow.SetActive(false);
            covidWindow.SetActive(true);
            joinWindow.SetActive(false);
            homeWindow.SetActive(false);
            requestWindow.SetActive(false);
            loginWindow.SetActive(false);
            provinceWindow.SetActive(false);
            cityWindow.SetActive(false);
        }
        public void joinUs()
        {
            aboutUsWindow.SetActive(false);
            nrcBorderWindow.SetActive(false);
            mapWindow.SetActive(false);
            covidWindow.SetActive(false);
            joinWindow.SetActive(true);
            homeWindow.SetActive(false);
            requestWindow.SetActive(false);
            loginWindow.SetActive(false);
            provinceWindow.SetActive(false);
            cityWindow.SetActive(false);
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
            requestWindow.SetActive(false);
            loginWindow.SetActive(false);
            provinceWindow.SetActive(false);
            cityWindow.SetActive(false);
        }

    #endregion

    #region LogInButtons and inner buttons

    public void logIn()
    {
        loginWindow.SetActive(true);
        homeWindow.SetActive(false);
    }
    public void requestRecord()
    {
        if (outposts.value == 0)
        {
            errorRequestText.SetActive(true);
        }
        else if(outposts.value == 1)
        {
            
            if (times.value == 2)
            {
                //do the thing
                Debug.Log("printing");
                OpenPassportWindows(Resources.LoadAll("Location3Passports", typeof(Sprite)));
                errorRequestText.SetActive(false);
            }
            else
            {
                errorRequestText.SetActive(true);
            }
        }
        else
        {
            errorRequestText.SetActive(true);
        }
    }
    

    public void requestPage()
    {
        requestWindow.SetActive(true);
        loginWindow.SetActive(false);
        provinceWindow.SetActive(false);
        cityWindow.SetActive(false);
    }

    public void provinceGet()
    {
        requestWindow.SetActive(false);
        loginWindow.SetActive(false);
        provinceWindow.SetActive(true);
        cityWindow.SetActive(false);
        
        back2Prov.SetActive(false);
        turn2Gether.SetActive(true);
        provOne.SetActive(false);
        provTwo.SetActive(false);
        provThree.SetActive(false);
        provFour.SetActive(false);
        provFive.SetActive(false);
        provSix.SetActive(false);
    }

    public void cityGet()
        {
            requestWindow.SetActive(false);
            loginWindow.SetActive(false);
            provinceWindow.SetActive(false);
            cityWindow.SetActive(true);
        }

    #endregion

    #region request passort uses

    public int OpenPassportWindows(object[] array)
        {
            Sprite[] sprites = new Sprite[array.Length];
            for (int x = 0; x < array.Length; x++)
            {
                sprites[x] = (Sprite)array[x];
            }
    
            StartCoroutine(StaggerCreatePassportWindows(sprites, 0.2f));
    
            return array.Length;
        }
    
        IEnumerator StaggerCreatePassportWindows(Sprite[] sprites, float delay)
        {
            int offset = 32;
            int i = 0;
            foreach (Sprite s in sprites)
            {
                FileWindow newWindow = Instantiate(NewWindowPrefab, ComputerPanel.transform);
                newWindow.LoadContents(s, s.name);
                newWindow.Stagger(i, offset);
                newWindow.transform.SetAsLastSibling();
                i++;
                yield return new WaitForSeconds(delay);
            }
        }

    #endregion

    #region provinces

    public void buttonOne()
    {
        provOne.SetActive(true);
        turn2Gether.SetActive(false);
        back2Prov.SetActive(true);
    }
    public void buttonTwo()
    {
        provTwo.SetActive(true);
        turn2Gether.SetActive(false);
        back2Prov.SetActive(true);
    }
    public void buttonThree()
    {
        provThree.SetActive(true);
        turn2Gether.SetActive(false);
        back2Prov.SetActive(true);
    }
    public void buttonFour()
    {
        provFour.SetActive(true);
        turn2Gether.SetActive(false);
        back2Prov.SetActive(true);
    }
    public void buttonFive()
    {
        provFive.SetActive(true);
        turn2Gether.SetActive(false);
        back2Prov.SetActive(true);
    }
    public void buttonSix()
    {
        provSix.SetActive(true);
        turn2Gether.SetActive(false);
        back2Prov.SetActive(true);
    }

    public void goBackToProv()
    {
        back2Prov.SetActive(false);
        turn2Gether.SetActive(true);
        provOne.SetActive(false);
        provTwo.SetActive(false);
        provThree.SetActive(false);
        provFour.SetActive(false);
        provFive.SetActive(false);
        provSix.SetActive(false);
    }
    #endregion
}
