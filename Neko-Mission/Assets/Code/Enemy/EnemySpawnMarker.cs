using UnityEngine;

public class EnemySpawnMarker : MonoBehaviour
{
    public Player Player;
    public Transform Marker;

    public float MinY = -0.5f;
    public float MaxY = 1.5f;
    public float Speed = 1f;
    
    private void Update()
    {
        Vector3 position = Marker.position;
        if (position.y < MinY || position.y > MaxY)
        {
            position.y = Mathf.Clamp(position.y, MinY, MaxY);
            Speed *= -1;
        }
        position.y += Speed * Time.deltaTime;
        Marker.position = position;
        
        Marker.LookAt(Player.transform);
    }
}
