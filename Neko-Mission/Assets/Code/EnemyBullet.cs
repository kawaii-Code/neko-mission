using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var Pl = collision.gameObject.GetComponent<Player>();
            // Debug.Log("Collided with Player");
            Pl.CurrentHealth -= 10;
            Destroy(this.gameObject);
            Debug.Log(Pl.CurrentHealth);
            if (Pl.CurrentHealth <= 0)
            {
                LoseMenu.Show();
                Pl.CurrentHealth = Pl.StartingHealth;
            }

        }

    }
}
