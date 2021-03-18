using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrintRow : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public void AssignText(string s)
    {
        Text.text = s;
    }
}
