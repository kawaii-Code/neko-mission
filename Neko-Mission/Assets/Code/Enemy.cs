using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float EnemySpeed;
    public int MaxHealth = 100;
    private int _health;


    void Start()
    {
        _health = MaxHealth;
    }

    // Получение урона
    public void TakingDamage(int Damage) {
        _health -= Damage;

        if (_health <= 0){
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // При столкновение с игроком 
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
