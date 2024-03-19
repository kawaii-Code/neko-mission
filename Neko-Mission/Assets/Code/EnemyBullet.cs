using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // Debug.Log("Collided with Player");
            player.TakeDamage(10);
            Destroy(this.gameObject);
        }
    }
}
