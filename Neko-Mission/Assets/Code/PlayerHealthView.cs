using TMPro;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
    public TMP_Text Text;
    
    public void Show(int health)
    {
        Text.text = $"{health}";
    }
}
