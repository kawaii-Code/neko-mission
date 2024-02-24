using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBarScript : MonoBehaviour
{
    public Player Pl;
    public TextMeshProUGUI BalanceText;

    void Update()
    {
        BalanceText.text = $"Баланс: {Pl.CurrentBalance}";
    }
}
