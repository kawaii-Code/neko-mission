using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBarScript : MonoBehaviour
{
    public Player Pl;
    private TextMeshProUGUI _balanceText;

    // Start is called before the first frame update
    void Start()
    {
        _balanceText = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _balanceText.text = $"Баланс: {Pl.CurrentBalance}";
    }
}
