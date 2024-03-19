using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerHealthScript : MonoBehaviour
{
    public Player Pl;
    public TextMeshProUGUI HealthText;
    void Update()
    {
        HealthText.text = $"{Pl.CurrentHealth}";
    }
}
