using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFadeOut : MonoBehaviour
{
    public Image blackOutSqure;
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut(float fadeSpeed = 0.1f)
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
