using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundInAnim : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    public void PlaySound()
    {
        if (AudioManager.Instance.SFXSource && clip)
        {
            AudioManager.Instance.SFXSource.PlayOneShot(clip);
        }
    }
}
