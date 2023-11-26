using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] AudioClips;

    public Dictionary<string, AudioClip> Clips;
    
    private static Sounds _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
        }

        _instance = this;
        Clips = AudioClips.ToDictionary(c => c.name);
        DontDestroyOnLoad(this);
    }

    public static void Play(string clipName)
    {
        if (_instance.Clips.TryGetValue(clipName, out AudioClip clip))
        {
            _instance.AudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError($"Звук '{clipName}' не найден!");
        }
    }
}