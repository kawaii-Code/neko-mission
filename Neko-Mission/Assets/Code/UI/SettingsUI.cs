using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Slider SensitivitySlider;
    public Slider MusicVolumeSlider;
    public Slider SoundVolumeSlider;
    
    public TextMeshProUGUI SensitivityValueLabel;
    public TextMeshProUGUI MusicVolumeValueLabel;
    public TextMeshProUGUI SoundVolumeValueLabel;

    private void Start()
    {
        UpdateSliders();
        UpdateLabels();
    }

    public void SetSensitivity(Slider slider)
    {
        Settings.CameraSensitivity.Value =
            Mathf.Lerp(Settings.CameraSensitivityMin, Settings.CameraSensitivityMax, slider.value);
        UpdateLabels();
    }

    public void SetMusicVolume(Slider slider)
    {
        Settings.MusicVolume.Value = slider.value;
        UpdateLabels();
    }

    public void SetSoundVolume(Slider slider)
    {
        Settings.SoundVolume.Value = slider.value;
        UpdateLabels();
    }

    private void UpdateSliders()
    {
        SensitivitySlider.value = Mathf.InverseLerp(Settings.CameraSensitivityMin, Settings.CameraSensitivityMax,
            Settings.CameraSensitivity);
        MusicVolumeSlider.value = Settings.MusicVolume;
        SoundVolumeSlider.value = Settings.SoundVolume;
    }

    private void UpdateLabels()
    {
        float cameraSensitivity = Settings.CameraSensitivity;
        
        SensitivityValueLabel.text = $"{cameraSensitivity:f1}";
        MusicVolumeValueLabel.text = $"{(int)(100 * Settings.MusicVolume)}";
        SoundVolumeValueLabel.text = $"{(int)(100 * Settings.SoundVolume)}";
    }

    public void PlayClick()
    {
        Sounds.PlayClick();
    }
}
