using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour
{
    public static SoundMan me;
    public int maxAudioSources;
    AudioSource[] sources;
    public AudioSource sourcePrefab;
    private Vector3 aPosition;
    
    [Header("PC SFX")]
    public AudioClip click;
    public AudioClip[] console;
    private int lastConsole = 0;
    public AudioClip emailReceived;
    
    [Header("Interaction SFX")]
    public AudioClip board;
    public AudioClip book;
    public AudioClip computer;
    public AudioClip newspaper;
    public AudioClip evidenceOnDrop;
    
    [Header("Other SFX")]
    public AudioClip ambience;
    public AudioClip printing;
    public AudioClip bookTurnPage;
    public AudioClip newspaperTurnPage;

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
            sources[i] = Instantiate(sourcePrefab,transform);
        }

        aPosition = transform.position;
        AmbienceSound();

        DontDestroyOnLoad(transform.gameObject);
    }

    public void PrintingSound()
    {
        PlaySoundAtPosition(printing, aPosition);
    }
    public void EmailReceivedSound()
    {
        PlaySoundAtPosition(emailReceived, aPosition);
    }
    public void BoardSound(Vector3 pos)
    {
        PlaySoundAtPosition(board,pos);
    }
    public void BookSound(Vector3 pos)
    {
        PlaySoundAtPosition(book,pos);
    }
    public void ComputerSound(Vector3 pos)
    {
        PlaySoundAtPosition(computer,pos);
    }
    public void NewspaperSound(Vector3 pos)
    {
        PlaySoundAtPosition(newspaper,pos);
    }

    public void EvidenceDropSound(Vector3 pos)
    {
        PlaySoundAtPosition(evidenceOnDrop, pos);
    }
    public void AmbienceSound()
    {
        AudioSource source = GetSource();
        source.gameObject.name = "Ambience";
        source.gameObject.AddComponent<AudioLowPassFilter>();
        source.gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000f;
        source.loop = true;
        source.volume = .5f;
        source.clip = ambience;
        source.transform.position = aPosition;
        source.Play();
    }

    public void AmbienceZoomIn()
    {
        GameObject ab = GameObject.Find("Ambience");
        ab.GetComponent<AudioLowPassFilter>().cutoffFrequency = 4000f;
    }
    
    public void AmbienceZoomOut()
    {
        GameObject ab = GameObject.Find("Ambience");
        ab.GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000f;
    }

    public void BookTurnSound()
    {
        PlaySoundAtPosition(bookTurnPage, aPosition);
    }

    public void NewspaperTurnSound()
    {
        PlaySoundAtPosition(newspaperTurnPage, aPosition);
    }
    public void ConsoleSound()
    {
        lastConsole = PlaySoundAtPosition(console, lastConsole, aPosition);
    }
    
    public void ClickSound()
    {
        PlaySoundAtPosition(click, aPosition);
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