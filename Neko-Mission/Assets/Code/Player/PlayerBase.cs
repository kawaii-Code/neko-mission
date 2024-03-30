using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    public int MaxHealth = 5;
    public HUD Hud;

    private int _health;

    private void Start()
    {
        _health = MaxHealth;
        Hud.ShowBaseHealth(_health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
            other.GetComponent<Enemy>().Die();
        }
    }

    private void TakeDamage(int damage = 1)
    {
        Sounds.Play("heartbeat");
        
        _health -= damage;
        if (_health <= 0)
        {
            LoseMenu.Show();
        }

        Hud.ShowBaseHealth(_health);
    }
}
