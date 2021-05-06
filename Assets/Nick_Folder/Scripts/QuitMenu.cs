using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour
{
    public Button quitButton;
    public Button cancelButton;

    bool doLerp = false;
    bool toDisplay;

    float currentLerpTime = 0;
    public float lerpScaleDuration;
    public Vector2 xOn, xOff;

    private void Awake()
    {
        quitButton.onClick.AddListener(() => QuitGame());
        cancelButton.onClick.AddListener(() => DisplayMenu(false));
    }

    void Start()
    {
        
    }

    void QuitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }

    public void DisplayMenu(bool show)
    {
        currentLerpTime = 0;
        if (show)
        {
            doLerp = true;
            toDisplay = true;
        }
        else
        {
            doLerp = true;
            toDisplay = false;
        }
    }

    private void Update()
    {
        if (doLerp)
        {
            float t = currentLerpTime / lerpScaleDuration;
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpScaleDuration)
                currentLerpTime = lerpScaleDuration;

            if (toDisplay)
                this.transform.localPosition = Vector3.Lerp(xOff, xOn, t);
            else
                this.transform.localPosition = Vector3.Lerp(xOn, xOff, t);
        }
    }
}
