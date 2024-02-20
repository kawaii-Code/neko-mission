using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public PlayerCamera PlayerCamera;
    public float FireRateBulletsPerSecond = 2f;
    public PlayerBullet BulletPrefab;
    public Transform ShootPoint;

    public bool Paused;
    
    private float ShootCooldown =>
        1 / FireRateBulletsPerSecond;

    private float _timeSinceLastShot;

    private void Update()
    {
        if (Paused)
            return;
        
        bool isShooting = Input.GetMouseButtonDown(0);
        
        if (isShooting && _timeSinceLastShot > ShootCooldown)
        {
            Shoot();
            _timeSinceLastShot = 0.0f;
        }
        
        _timeSinceLastShot += Time.deltaTime;
    }

    private void Shoot()
    {
        PlayerBullet bullet = Instantiate(BulletPrefab, ShootPoint.position, Quaternion.identity);
        bullet.transform.forward = PlayerCamera.transform.forward;
    }
}