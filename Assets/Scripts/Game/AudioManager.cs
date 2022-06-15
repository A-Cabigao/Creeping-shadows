using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> playerSoundEffects;

    public static AudioManager instance;

    private float volume;


    private void Awake()
    {
        instance = this;
        volume = 1f;
    }

    // Plays an audio from player sound effects list
    public void PlayPlayerAudio(string clipName, Transform objectTransform)
    {
        if(objectTransform != null)
        {
            foreach(AudioClip clip in playerSoundEffects)
            {
                if(clip.name.Equals(clipName))
                {
                    AudioSource audioSource;
                    if(objectTransform.gameObject.GetComponent<AudioSource>() == null)
                    {
                        audioSource = objectTransform.gameObject.AddComponent<AudioSource>();
                        audioSource.clip = clip;
                        audioSource.volume = volume;
                        audioSource.PlayOneShot(audioSource.clip);
                    }
                    else
                    {
                        audioSource = objectTransform.GetComponent<AudioSource>();
                        audioSource.clip = clip;
                        audioSource.volume = volume;
                        audioSource.PlayOneShot(audioSource.clip);
                    }
                    return;
                }
            }

            Debug.Log("clip name not found");
            return;
        }
        else
        {
            Debug.Log("transform is null!");
            return;
        }
    }

    public void SetSoundEffectVolume(float value)
    {
        volume = value;
    }
}
