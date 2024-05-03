using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource MenuMusicSource;
    public AudioSource BattleMusicSource;

    private void Start()
    {
        OnMusicVolumeChanged(Settings.MusicVolume);
    }

    private void OnEnable()
    {
        Settings.MusicVolume.Changed += OnMusicVolumeChanged;
    }
    
    private void OnDisable()
    {
        Settings.MusicVolume.Changed -= OnMusicVolumeChanged;
    }
    
    public void PlayBattleMusic()
    {
        if (BattleMusicSource.isPlaying)
            return;
        
        BattleMusicSource.volume = Settings.MusicVolume;
        BattleMusicSource.Play();
        
        MenuMusicSource.volume = 0.0f;
        MenuMusicSource.Stop();
    }
    
    public void PlayMenuMusic()
    {
        if (MenuMusicSource.isPlaying)
            return;
        
        BattleMusicSource.volume = 0.0f;
        BattleMusicSource.Stop();
        
        MenuMusicSource.volume = Settings.MusicVolume;
        MenuMusicSource.Play();
    }

    private void OnMusicVolumeChanged(float newValue)
    {
        MenuMusicSource.volume = newValue;
        BattleMusicSource.volume = newValue;
    }
}