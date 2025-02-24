using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootStepSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    public void PlayFootstepSound()
    {
        audioSource.PlayOneShot(clip);
    }

}
