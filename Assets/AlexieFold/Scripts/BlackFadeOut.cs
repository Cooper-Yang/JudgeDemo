using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackFadeOut : MonoBehaviour
{
    public Image blackOutSqure;
    public Image blackInSquare;
    public TextMeshProUGUI continues;
    public Button restart;
    public Button exitGame;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            StartCoroutine(FadeOut());
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(EndGame());
            restart.gameObject.SetActive(false);
            exitGame.gameObject.SetActive(false);
        }
        }

    // Update is called once per frame
    void Update()   
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void callFadeEnding()
    {
        StartCoroutine(FadeEnding());
    }

    IEnumerator EndGame(float fadeSpeed = 0.4f)
    {
        Color objectColor = continues.GetComponent<TextMeshProUGUI>().color;
        float fadeAmount;
        while (continues.GetComponent<TextMeshProUGUI>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            continues.GetComponent<TextMeshProUGUI>().color = objectColor;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            continues.transform.position += Vector3.up * Time.deltaTime * 20;
            if (continues.transform.position.y >= 300f)
                break;
            yield return null;
        }
        restart.gameObject.SetActive(true);
        exitGame.gameObject.SetActive(true);
        Color resColor = restart.GetComponentInChildren<TextMeshProUGUI>().color;
        Color exitColor = restart.GetComponentInChildren<TextMeshProUGUI>().color;
        while (restart.GetComponentInChildren<TextMeshProUGUI>().color.a < 1)
        {
            fadeAmount = resColor.a + (fadeSpeed * Time.deltaTime);

            resColor = new Color(resColor.r, resColor.g, resColor.b, fadeAmount);
            restart.GetComponentInChildren<TextMeshProUGUI>().color = resColor;
            exitGame.GetComponentInChildren<TextMeshProUGUI>().color = resColor;
            yield return null;
        }
    }

    IEnumerator FadeEnding(float fadeSpeed = 0.4f)
    {
        Color objectColor = blackInSquare.GetComponent<Image>().color;
        float fadeAmount;
        while (blackInSquare.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackInSquare.GetComponent<Image>().color = objectColor;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(2);
    }

    IEnumerator FadeOut(float fadeSpeed = 0.5f)
    {
        Color objectColor = blackOutSqure.GetComponent<Image>().color;
        float fadeAmount;
        while (blackOutSqure.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSqure.GetComponent<Image>().color = objectColor;
            yield return null;
        }
    }
}
