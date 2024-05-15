using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float FireRange;
    public float FireRate;
    public float BulletSpeed;
    public int Damage;
    public int Price;
    public int IncreaseDamagePrice;
    public int IncreaseFireRatePrice;
    public int AddSlowdownPrice;
    // Лист врагов в области
    [HideInInspector]
    public List<GameObject> _nearEnemy = new List<GameObject>();
    [HideInInspector]
    public float _timer;
    [HideInInspector]
    public Vector3 _towerSize;
    [HideInInspector]
    public MeshRenderer _renderer;
    [HideInInspector]
    public Vector3 _genPos;
    // Хочется сделать приватными но не будет наследования, а без него ругается 🤬🤬

    // Получение ближайшего врага в области
    public GameObject GetClosestEnemy(GameObject[] objects)
    {
        GameObject bestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject currentObject in objects)
        {
            if (currentObject)
            {
                Vector3 differenceToTarget = currentObject.transform.position - _genPos;
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
