using TMPro;
using UnityEngine;

public class WaveInfoWidget : MonoBehaviour
{
    public GameObject RestPanel;
    public GameObject WavePanel;
    
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI WaveRemainingTimeText;
    public TextMeshProUGUI RemainingText;

    public void ShowRemainingRestTime(float time)
    {
        RestPanel.SetActive(true);
        WavePanel.SetActive(false);
        TimerText.text = $"{(int)time / 60:00}:{(int)time % 60:00}";
    }

    public void ShowRemainingWaveTime(float time)
    {
        WaveRemainingTimeText.text = $"{(int)time / 60:00}:{(int)time % 60:00}";
    }

    public void ShowRemainingEnemies(int current, int max)
    {
        RestPanel.SetActive(false);
        WavePanel.SetActive(true);
        RemainingText.text = $"{current}/{max}";
    }
}