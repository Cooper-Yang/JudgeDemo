using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class FileWindow : MonoBehaviour
{
    [SerializeField] Button CloseButton;
    [SerializeField] Button SaveButton;

    [SerializeField] Image ContentImage;
    [SerializeField] TextMeshProUGUI FileNameText;

    // File myFile;

    private void Awake()
    {
        CloseButton.onClick.AddListener(() => DestoryThisWindow());
        SaveButton.onClick.AddListener(() => SaveToDesktop());
    }

    public void ReadFile(string path)
    {
        Sprite s = (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));

        ContentImage.sprite = s;

        string name = "New Window";
        int lastIndex = path.LastIndexOf("/");
        if (lastIndex != -1)
            name = path.Substring(lastIndex + 1);

        FileNameText.text = name;
    }

    /*private void UpdateWindow()
    {
        //ContentImage.sprite = myFile.GetContentSprite();
        //FileNameText.text = myFile.GetFileName();
    }*/

    private void DestoryThisWindow()
    {
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void SaveToDesktop()
    {

    }

}
