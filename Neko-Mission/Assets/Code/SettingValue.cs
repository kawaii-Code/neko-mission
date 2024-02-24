using System;

// Обёртка над значением, которое задаётся в настройках.
// Сделано, чтобы значения автоматически обновлялись если
// пользователь поменяет что-то в настройках.
public class SettingValue<T>
{
    private T _value;
    
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            Changed?.Invoke(_value);
        }
    }

    public event Action<T> Changed;

    public SettingValue(T defaultValue)
    {
        Value = defaultValue;
    }

    public static implicit operator T(SettingValue<T> setting)
    {
        return setting.Value;
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }
}