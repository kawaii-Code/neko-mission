using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBarScript : MonoBehaviour
{
    public Player Pl;
    public TextMeshProUGUI PlayerBalance;

    void Update()
    {
        PlayerBalance.text = $"Баланс: {Pl.CurrentBalance}";
    }
}
