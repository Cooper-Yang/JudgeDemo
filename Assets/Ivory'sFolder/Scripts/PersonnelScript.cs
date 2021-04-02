using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PersonnelScript : MonoBehaviour
{
    [Header("Assign")]
    [SerializeField]
    private Image PersonImg;
    [SerializeField]
    private TextMeshProUGUI NameTxt;
    [SerializeField]
    private TextMeshProUGUI IDTxt;
    [SerializeField]
    private TextMeshProUGUI IdentityTxt;
    [SerializeField]
    private TextMeshProUGUI JobTxt;
    [SerializeField]
    private TextMeshProUGUI AddressTxt;
    [SerializeField]
    private TextMeshProUGUI CriminalTxt;
    [Header("Input")]
    [SerializeField]
    private Sprite PersonSprite;
    [SerializeField]
    private string Name;
    [SerializeField]
    private string IDNum;
    [SerializeField]
    private string Identity;
    [SerializeField]
    private string Job;
    [SerializeField]
    private string Address;
    [SerializeField]
    private string Criminal;

    void Awake()
    {
        PersonImg.sprite = PersonSprite;
        NameTxt.text += Name;
        IDTxt.text += IDNum;
        IdentityTxt.text += Identity;
        JobTxt.text += Job;
        AddressTxt.text += Address;
        CriminalTxt.text += Criminal;
    }


}
