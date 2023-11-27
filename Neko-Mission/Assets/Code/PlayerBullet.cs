using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed;
    public int Damage;
    
    private void Update()
    {
        transform.position += transform.forward * (Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}