using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int Damage;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // Debug.Log("Collided with Player");
            player.TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}
