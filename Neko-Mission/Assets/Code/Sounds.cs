using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public GameObject Dummy;
    public AudioSource AudioSource;
    public AudioClip[] AudioClips;
    public Music Music;

    public Dictionary<string, AudioClip> Clips;
    
    private static Sounds _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        Clips = AudioClips.ToDictionary(c => c.name);
        DontDestroyOnLoad(this);
        
        PlayMenuMusic();
    }

    public static void Play(string clipName)
    {
        if (!_instance)
        {
            Debug.LogWarning("Невозможно проиграть звук. Начните игру с главному меню чтобы звуки появились.");
            return;
        }
        
        if (_instance.Clips.TryGetValue(clipName, out AudioClip clip))
        {
            _instance.AudioSource.volume = Settings.SoundVolume;
            _instance.AudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError($"Звук '{clipName}' не найден!");
        }
    }

    public static void PlayAt(string clipName, Vector3 position)
    {
        GameObject go = Instantiate(_instance.Dummy, position, Quaternion.identity);
        AudioSource source = go.GetComponent<AudioSource>();
        if (_instance.Clips.TryGetValue(clipName, out AudioClip clip))
        {
            source.volume = Settings.SoundVolume;
            source.PlayOneShot(clip);
            Destroy(go, clip.length);
        }
    }

    public static void PlayBattleMusic()
    {
        _instance.Music.PlayBattleMusic();
    }
    
    public static void PlayMenuMusic()
    {
        _instance.Music.PlayMenuMusic();
    }

    public static void PlayClick()
    {
        Play("cute-click");
    }
    
    public static void PlayClose()
    {
        Play("close-click");
    }
}