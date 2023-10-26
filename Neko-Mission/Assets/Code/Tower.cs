using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float FireRange;
    public float FireRate;
    public int Damage;

    private List<GameObject> NearEnemy = new List<GameObject>();

    private float _timer;

    private void Start()
    {
        gameObject.AddComponent<SphereCollider>();
        var myCollider = GetComponent<SphereCollider>();
        myCollider.radius = FireRange;
        myCollider.isTrigger = true;
    }

    private void Update()
    {
        if(_timer > FireRate){
            _timer = 0.0f;
        }

        _timer += Time.deltaTime;    
    }

    private void OnOnTriggerStay(Collider other){
        if (other)
        {
            if (!NearEnemy.Contains(other.gameObject))
            {
                NearEnemy.Add(other.gameObject);
            }
        }
        Debug.Log(NearEnemy);
    }
}
