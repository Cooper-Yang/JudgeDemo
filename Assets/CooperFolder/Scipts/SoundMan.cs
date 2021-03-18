using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour
{
    public static SoundMan me;
    public int maxAudioSources;
    AudioSource[] sources;
    public AudioSource sourcePrefab;
    public AudioClip[] clickClips;
    private int lastClick;

    private void Awake()
    {
        if (me != null)
        {
            Destroy(gameObject); 
            return;
        }
        me = this;

        sources = new AudioSource[maxAudioSources];
        for (int i = 0; i < maxAudioSources; i++)
        {
            sources[i] = Instantiate(sourcePrefab);
        }
    }

    public void Click(Vector3 pos)
    {
        lastClick = PlaySoundAtPosition(clickClips, lastClick, pos);
    }

    private void PlaySoundAtPosition(AudioClip clip, Vector3 pos) //play single clip
    {
        AudioSource source = GetSource();
        source.clip = clip;
        source.transform.position = pos;
        source.pitch = Random.Range(.925f, 1.075f); //optional
        source.Play();
        
    }
    
    private int PlaySoundAtPosition(AudioClip[] clips, int lastPlayed, Vector3 pos) //play clip in a clip list
    {
        AudioSource source = GetSource();
        int clipNum = GetClipindex(clips.Length, lastPlayed);

        source.clip = clips[clipNum];
        source.transform.position = pos;
        source.pitch = Random.Range(.925f, 1.075f);
        source.Play();

        return clipNum;
    }


    private int GetClipindex(int clipNum, int lastPlayed)
    {
        int num = Random.Range(0, clipNum);
        while (num == lastPlayed)
        {
            num = Random.Range(0, clipNum);
        }
        return num;
    }

    private AudioSource GetSource()  //get source that is not playing
    {
        for (int i = 0; i < maxAudioSources; i++)
        {
            if (!sources[i].isPlaying)
            {
                return sources[i];
            }
        }
        Debug.LogError("NOT ENOUGH SOURCES");
        return sources[0];
    }
}