using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : Singleton<AudioManager>
{
    [Header("-------- Audio Source --------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;
    [Header("-------- Audio Clip --------")]
    public AudioClip mainMenuMusic;
    public AudioClip attackSound;
    public AudioClip throwSound;
    public AudioClip dashSound;
    public AudioClip finalAttackSound;
    public AudioClip healthSound;
    public AudioClip RunSound;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
}
