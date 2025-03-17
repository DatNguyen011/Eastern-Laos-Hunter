using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerManager : Singleton<AudioMixerManager>
{

    public AudioMixer audioMixer;
    public Slider SFXSilder;
    public Slider musicSilder;

    void Awake()
    {
        
        float getSlideMusic = PlayerPrefs.GetFloat("music", 1f);
        float getSlideSFX = PlayerPrefs.GetFloat("sfx", 1f);
        musicSilder.value = getSlideMusic;
        SFXSilder.value = getSlideSFX;
        SetMusicVolume(getSlideMusic);
        SetVFXVolume(getSlideSFX);
    }

    void Start()
    {
        
    }


    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("music", volume);
        PlayerPrefs.Save();
    }

    public void SetVFXVolume(float volume)
    {
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx", volume);
        PlayerPrefs.Save();
    }
}
