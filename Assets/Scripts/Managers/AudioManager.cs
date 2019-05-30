using UnityEngine;
using Utils;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource musicstream;
    public AudioSource soundstream;

    protected override void Awake()
    {
        base.Awake();
        if(null != musicstream && null != musicstream.clip)
            musicstream.Play();
    }

    public void PlayMusic(AudioClip soundClipToPlay)
    {
        musicstream.clip = soundClipToPlay;
        musicstream.Play();
    }

    public void PlaySound (AudioClip soundClipToPlay)
    {
        soundstream.clip = soundClipToPlay;
        soundstream.Play();
    }
}
