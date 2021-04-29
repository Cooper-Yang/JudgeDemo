using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    public TMP_InputField password;
    public TextMeshProUGUI warning;
    public TextMeshProUGUI hint;
    public TextMeshProUGUI title;
    public Image blackOutSqure;
    public Camera mainCamera;
    private int wrongTries;
    private float fadeOutTimer;
    private bool playTitle;
    // Start is called before the first frame update
    void Start()
    {
        wrongTries = 0;
        fadeOutTimer = 0;
        playTitle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (wrongTries >= 3)
            hint.text = "Hint: Birthday MMDD";
        if (playTitle)
        {
            fadeOutTimer += 0.08f * Time.deltaTime;
            AmbienceFadeOut();
        }
    }

    public void StartButton()
    {
        if (password.text == "1104")
        {
            playTitle = true;
            StopAllCoroutines();
            StartCoroutine(PlayTitle());
            Debug.Log("Password correct");
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(StartWarning());
            if(password.text != "")
                wrongTries += 1;
        }
    }
    public void AmbienceFadeOut()
    {
        GameObject ab = GameObject.Find("Ambience");
        ab.GetComponent<AudioLowPassFilter>().cutoffFrequency = Mathf.Lerp(4000f, 22000f, fadeOutTimer);
        ab.GetComponent<AudioSource>().volume = Mathf.Lerp(0.5f, 0.9f, fadeOutTimer);
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

    IEnumerator PlayTitle(float fadeSpeed = 0.2f)
    {
        Color objectColor = blackOutSqure.GetComponent<Image>().color;
        Color titleColor = title.color;
        float fadeAmount;

        if (playTitle)
        {
            while(blackOutSqure.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSqure.GetComponent<Image>().color = objectColor;

                mainCamera.orthographicSize += 3 * Time.deltaTime;
                yield return null;
            }

            if (blackOutSqure.GetComponent<Image>().color.a >= 1)
            {
                while (title.color.a < 1)
                {
                    fadeAmount = titleColor.a + (fadeSpeed * Time.deltaTime);

                    titleColor = new Color(titleColor.r, titleColor.g, titleColor.b, fadeAmount);
                    title.color = titleColor;
                    yield return null;
                }
            }

            yield return new WaitForSeconds(2f);

            if(title.color.a >= 1)
            {
                while(title.color.a > 0)
                {
                    fadeAmount = titleColor.a - (fadeSpeed * Time.deltaTime);

                    titleColor = new Color(titleColor.r, titleColor.g, titleColor.b, fadeAmount);
                    title.color = titleColor;

                    GameObject ab = GameObject.Find("Ambience");
                    ab.GetComponent<AudioSource>().volume = Mathf.Lerp(0.5f, 0.9f, title.color.a);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(0);
        }
    }
}
