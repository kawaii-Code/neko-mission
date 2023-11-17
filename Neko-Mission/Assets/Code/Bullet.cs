using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Target;
    public int Damage;
    public float Speed;
    
    private Enemy _actionTarget;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        transform.position = Vector3.Lerp(transform.position, Target.transform.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other)
        {
            if (other.tag == "Enemy")
            {
                var damageEnemy = other.GetComponent<Enemy>();
                damageEnemy.TakingDamage(Damage);
                
                Destroy(this.gameObject);
            } 
        }
    }
}
