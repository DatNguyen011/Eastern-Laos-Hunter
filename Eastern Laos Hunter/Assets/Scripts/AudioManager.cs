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
    public AudioClip attackMusic;
    public AudioClip throwMusic;
    public AudioClip dashMusic;
    public AudioClip finalAttackMusic;
    public AudioClip healthMusic;
    //public AudioClip hitMusic;

    private void Start()
    {
        //musicSource.clip = mainMenuMusic;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
