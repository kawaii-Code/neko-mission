using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Animator WaveAnimation;
    public TMP_Text WaveText;

    public TMP_Text HealthText;

    public void ShowBaseHealth(int health)
    {
        HealthText.text = $"{health}";
    }

    public void ShowNextWaveWarning(int wave)
    {
        WaveText.text = $"ВОЛНА {wave}";
        WaveAnimation.Play("WaveWarningAnimation");
    }
}
