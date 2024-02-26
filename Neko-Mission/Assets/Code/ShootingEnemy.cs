using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject enemyBullet;
    public float shootRange = 100f;
    public float shootInterval = 1f;
    public float shootTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= shootRange)
        {
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootInterval;
            }
            else
            {
                shootTimer -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        GameObject bul = Instantiate(enemyBullet, transform.position + Vector3.up * 2.0f, Quaternion.identity);
        bul.GetComponent<Rigidbody>().velocity = (player.position - bul.transform.position).normalized * 50f;
        Destroy(bul, 1);
    }


}
