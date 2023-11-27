using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    public int MaxHealth = 5;
    public PlayerHealthView View;

    private int _health;

    private void Start()
    {
        _health = MaxHealth;
        View.Show(_health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage();
            Destroy(other.gameObject);
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

        View.Show(_health);
    }
}
