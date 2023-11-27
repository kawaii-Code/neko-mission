using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public Camera Camera;
    public float FireRateBulletsPerSecond = 2f;
    public PlayerBullet BulletPrefab;
    public Transform ShootPoint;

    private float ShootCooldown =>
        1 / FireRateBulletsPerSecond;

    private float _timeSinceLastShot;

    private void Update()
    {
        bool isShooting = Input.GetMouseButtonDown(1);
        
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
        bullet.transform.forward = Camera.transform.forward;
    }
}