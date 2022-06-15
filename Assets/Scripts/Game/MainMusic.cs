using UnityEngine;

public class MainMusic : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMusicVolume(float value)
    {
        audioSource.volume = value;
    }
}
