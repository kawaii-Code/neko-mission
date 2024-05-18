using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{   
    private float _baseFireRate;
    private float _fireRateBonus = 30; // %
    private GameObject _booster;
    private bool _isSpedUp;

    private void Start()
    {
        IncreaseDamagePrice = 10;
        IncreaseFireRatePrice = 20;
        _baseFireRate = FireRate;
        // Создание триггера ( коллайдера )
        gameObject.AddComponent<SphereCollider>();
        var myCollider = GetComponent<SphereCollider>();
        myCollider.center = new Vector3(0f,-0.5f,0f);
        myCollider.radius = FireRange;
        myCollider.isTrigger = true;

        // Сохранение размеров башни
        _renderer = GetComponent<MeshRenderer>();
        _towerSize = _renderer.bounds.size;

        _genPos = this.transform.position;
        _genPos.y += 0.5f * _towerSize.y;
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
                Sounds.PlayAt("arrow-shot", transform.position);
                var bullet = Instantiate(BulletPrefab, _genPos, Quaternion.LookRotation(target.transform.position - transform.position));
                var componentBullet = bullet.GetComponent<Bullet>();

                // установка параметров пули
                componentBullet.Damage = Damage;
                componentBullet.Speed = BulletSpeed;
                componentBullet.Target = target;
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

    // Получение ближайшего врага в области
    GameObject GetClosestEnemy(GameObject[] objects)
    {
        GameObject bestTarget = null;
        float closestDistance = float.MaxValue;
        Vector3 currentPosition = transform.position;

        foreach (GameObject currentObject in objects)
        {
            if (currentObject)
            {
                Vector3 differenceToTarget = currentObject.transform.position - currentPosition;
                float distanceToTarget = differenceToTarget.sqrMagnitude;

                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    bestTarget = currentObject;
                }
            }
        }

        return bestTarget;
    }

    public override void SpeedUpFireRate()
    {
        _isSpedUp = true;
        FireRate -= _baseFireRate * (_fireRateBonus / 100);
        var _speedUpGen = _genPos;
        _speedUpGen.y += 3f;

        _booster = Instantiate(SpeedUpPrefab, _speedUpGen, Quaternion.AngleAxis(90, new Vector3(0,0,0)));
    }

    public override void SlowdownFireRate()
    {
        _isSpedUp = false;
        FireRate += _baseFireRate * (_fireRateBonus / 100);
        Destroy(_booster); 
    }
    public override bool HasBonusSpeed()
    {
        return _isSpedUp;
        // Жесть, 1 час это 💀
        return Math.Abs(_baseFireRate - FireRate) > 0.05; // 1 час из-за 0.8 == 0.8 - false 😖
    }
}