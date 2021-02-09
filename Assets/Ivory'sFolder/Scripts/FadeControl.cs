using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    public Material PixelMaterial;

    bool Started = false;

    float minSize = 0.001f;
    float maxSize = 0.1f;
    float speed = 0.002f;

    // Start is called before the first frame update
    void Start()
    {
        SetSize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeIn()
    {
        Started = true;
        yield return null;

        float size = maxSize;
        while(size > minSize)
        {
            size -= speed;
            PixelMaterial.SetFloat("_PixelSize", size);
            yield return null;
        }

    }

    IEnumerator FadeOut()
    {

        yield return null;

        float size = minSize;
        while (size < maxSize)
        {
            size += speed;
            PixelMaterial.SetFloat("_PixelSize", size);
            yield return null;
        }

        Started = false;
    }

    void SetSize()
    {
        PixelMaterial.SetFloat("_PixelSize", maxSize);
    }

}
