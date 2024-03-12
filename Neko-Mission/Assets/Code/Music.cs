using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource MusicSource;

    private void Start()
    {
        MusicSource.volume = Settings.MusicVolume;
        MusicSource.loop = true;
        MusicSource.Play();
    }

    private void OnEnable()
    {
        Settings.MusicVolume.Changed += OnMusicVolumeChanged;
    }
    
    private void OnDisable()
    {
        Settings.MusicVolume.Changed -= OnMusicVolumeChanged;
    }

    private void OnMusicVolumeChanged(float newValue)
    {
        MusicSource.volume = newValue;
    }
}