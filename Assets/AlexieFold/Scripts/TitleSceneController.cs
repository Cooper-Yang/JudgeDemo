using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    public TMP_InputField password;
    public TextMeshProUGUI warning;
    public TextMeshProUGUI hint;
    private int wrongTries;
    // Start is called before the first frame update
    void Start()
    {
        wrongTries = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (wrongTries >= 3)
            hint.text = "Hint: Birthday MMDD";
    }

    public void StartButton()
    {
        if (password.text == "1104")
            Debug.Log("Password correct");
        else
        {
            StopAllCoroutines();
            StartCoroutine(StartWarning());
            wrongTries += 1;
        }
    }

    IEnumerator StartWarning()
    {
        if (password.text == "")
            warning.text = "Please enter password";
        else
            warning.text = "Password incorrect";
        yield return new WaitForSeconds(2f);
        warning.text = "";
    }
}
