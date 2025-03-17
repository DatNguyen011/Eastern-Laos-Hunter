using System.Collections.Generic;
using UnityEngine;

public class PlaySoundInAnim : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;

    public void PlaySound(int clipIndex)
    {
        if (AudioManager.Instance.SFXSource && clips != null && clips.Count > 0)
        {
            if (clipIndex >= 0 && clipIndex < clips.Count)
            {
                AudioManager.Instance.SFXSource.PlayOneShot(clips[clipIndex]);
            }
            else
            {
                Debug.LogWarning("Error");
            }
        }
    }
}
