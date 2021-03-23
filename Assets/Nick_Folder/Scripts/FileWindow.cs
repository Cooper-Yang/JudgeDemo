using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FileWindow : MonoBehaviour
{
    [SerializeField] Button CloseButton;
    [SerializeField] Button PrintButton;

    [SerializeField] Image ContentImage;
    [SerializeField] TextMeshProUGUI FileNameText;

    // File myFile;

    private void Awake()
    {
        CloseButton.onClick.AddListener(() => DestoryThisWindow());
        PrintButton.onClick.AddListener(() => Print());
    }

    public void ReadFile(string path)
    {
        Sprite s = Resources.Load<Sprite>(path);
        Debug.Log(path);

        ContentImage.sprite = s;

        string name = "New Window";
        int lastIndex = path.LastIndexOf("/");
        if (lastIndex != -1)
            name = path.Substring(lastIndex + 1);

        FileNameText.text = name;
    }

    public void LoadContents(Sprite sprite, string fileName)
    {
        ContentImage.sprite = sprite;
        FileNameText.text = fileName;
    }

    public void Stagger(int count, float amount)
    {
        float newX = amount * count;
        float newY = amount * -count;
        this.transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
    }

    public void Print()
    {
        Printer printer = FindObjectOfType<Printer>();
        printer.Print(ContentImage.sprite, FileNameText.text);
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
