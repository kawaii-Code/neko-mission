public static class Settings
{
    public const float CameraSensitivityMax = 10.0f;
    public const float CameraSensitivityMin = 0.1f;

    public static SettingValue<float> CameraSensitivity = new(1.0f);
    public static SettingValue<float> SoundVolume = new(0.5f);
    public static SettingValue<float> MusicVolume = new(0.5f);
}