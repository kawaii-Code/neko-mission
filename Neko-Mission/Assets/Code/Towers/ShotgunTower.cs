using System.Collections.Generic;
using UnityEngine;
using System;

public class ShotgunTower : Tower
{
    public int BulletCount;
    public float MaxSpreadAngel; // максимальный разброс от врага
    private float _baseFireRate;
    private float _fireRateBonus = 20; // %

    private void Start()
    {
        _baseFireRate = FireRate;
        // Создание триггера ( коллайдера )
        gameObject.AddComponent<SphereCollider>();
        var myCollider = GetComponent<SphereCollider>();
        myCollider.center = new Vector3(0f,0.0f,0f);
        myCollider.radius = FireRange;
        myCollider.isTrigger = true;
        
        // Сохранение размеров башни
        _renderer = GetComponent<MeshRenderer>();
        _towerSize = _renderer.bounds.size;

        _genPos = transform.position;
        _genPos.y += _towerSize.y;
    }

    private void Update()
    {
        // Таймер
        if (_timer > FireRate)
        {
            // ближайший враг
            var target = GetClosestEnemy(_nearEnemy.ToArray());
            
            if (target) // без этого куча ошибок
            {
                Vector3 TargetPos = target.transform.position;

                float Radius = (float)Math.Round(Mathf.Tan(MaxSpreadAngel * Mathf.Deg2Rad) * Mathf.Sqrt(Mathf.Pow(TargetPos.x - _genPos.x, 2) + Mathf.Pow(TargetPos.y - _genPos.y, 2) + Mathf.Pow(TargetPos.z - _genPos.z, 2)),2);

                for (int i = 0; i < BulletCount; i++){

                    Vector3 SpreadTarget = TargetPos + UnityEngine.Random.insideUnitSphere * Radius;

                    var bullet = Instantiate(BulletPrefab, _genPos, Quaternion.LookRotation(SpreadTarget - transform.position));
                    var componentBullet = bullet.GetComponent<Bullet>();          

                    componentBullet.Damage = Damage;
                    componentBullet.Speed = BulletSpeed;
                    componentBullet.TargetPos = SpreadTarget;
                }
            }
            _nearEnemy = new List<GameObject>();
            _timer = 0.0f;
        }
        _timer += Time.deltaTime;
    }

    // Добавление врагов в лист
    private void OnTriggerStay(Collider other)
    {
        if (other)
        {
            if (!_nearEnemy.Contains(other.gameObject) && other.CompareTag("Enemy"))
            {
                _nearEnemy.Add(other.gameObject);
            }
        }
    }

    public override void SpeedUpFireRate() {
        FireRate -= _baseFireRate * (_fireRateBonus / 100);
    }

    public override void SlowdownFireRate() {
        FireRate += _baseFireRate * (_fireRateBonus / 100);
    }

    public override bool HasBonusSpeed() {
        return Math.Abs(_baseFireRate - FireRate) > 0.05;
    }
}
