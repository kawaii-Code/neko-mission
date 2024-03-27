using TMPro;
using UnityEngine;

public class WaveInfoWidget : MonoBehaviour
{
    public GameObject RestTime;
    public GameObject RemainingEnemies;
    
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI RemainingText;

    public void ShowRemainingRestTime(float time)
    {
        RestTime.SetActive(true);
        RemainingEnemies.SetActive(false);
        TimerText.text = time.ToString();
    }

    public void ShowRemainingEnemies(int current, int max)
    {
        RestTime.SetActive(false);
        RemainingEnemies.SetActive(true);
        RemainingText.text = $"{current} / {max}";
    }
}