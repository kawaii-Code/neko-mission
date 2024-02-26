using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float FireRange;
    public float FireRate;
    public float BulletSpeed;
    public int Damage;
    
    // Лист врагов в области
    private List<GameObject> _nearEnemy = new List<GameObject>();
    private float _timer;
    private Vector3 _towerSize;
    private MeshRenderer _renderer;
    
    private void Start()
    {
        // Создание триггера ( коллайдера )
        gameObject.AddComponent<SphereCollider>();
        var myCollider = GetComponent<SphereCollider>();
        myCollider.center = new Vector3(0f,-0.5f,0f);
        myCollider.radius = FireRange;
        myCollider.isTrigger = true;
        
        // Сохранение размеров башни
        _renderer = GetComponent<MeshRenderer>();
        _towerSize = _renderer.bounds.size;
    }

    private void Update()
    {
        // Таймер
        if (_timer > FireRate)
        {
            Vector3 genPos = this.transform.position;
            genPos.y += 0.5f * _towerSize.y;
            
            // ближайший враг
            var target = GetClosestEnemy(_nearEnemy.ToArray());
            
            if (target) // без этого куча ошибок
            {
                var bullet = Instantiate(BulletPrefab, genPos, Quaternion.LookRotation(target.transform.position - transform.position));
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
    GameObject  GetClosestEnemy(GameObject[] objects)
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

}
