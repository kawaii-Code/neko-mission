using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        Load();
    }
    
    private void OnDisable()
    {
        Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey("CameraSensitivity"))
            return;

        Settings.CameraSensitivity.Value = PlayerPrefs.GetFloat("CameraSensitivity");
        Settings.MusicVolume.Value = PlayerPrefs.GetFloat("MusicVolume");
        Settings.SoundVolume.Value = PlayerPrefs.GetFloat("SoundVolume");
        
        Settings.CameraSensitivity.Changed += x => Save();
        Settings.MusicVolume.Changed += x => Save();
        Settings.SoundVolume.Changed += x => Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("CameraSensitivity", Settings.CameraSensitivity);
        PlayerPrefs.SetFloat("MusicVolume", Settings.MusicVolume);
        PlayerPrefs.SetFloat("SoundVolume", Settings.SoundVolume);
    }
}
