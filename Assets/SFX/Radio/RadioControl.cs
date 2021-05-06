using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RadioControl : MonoBehaviour
{
    public AudioClip[] radioClips;
    private AudioSource radioSource;
    private int clipNum;

    void Start()
    {
        radioSource = GetComponent <AudioSource>();
        radioSource.clip = radioClips[0];
        clipNum = 0;
        radioSource.Play();
    }

    void Update()
    {
        if (!radioSource.isPlaying)
        {
            if (clipNum == radioClips.Length - 1)
            {
                radioSource.clip = radioClips[0];
                clipNum = 0;
                radioSource.Play();
            }
            else
            {
                clipNum += 1;
                radioSource.clip = radioClips[clipNum];
                radioSource.Play();
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
            PauseOrResume();
    }


    public void PauseOrResume()
    {
        radioSource.mute = !radioSource.mute;
    }
}
