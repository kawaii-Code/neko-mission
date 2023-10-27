using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float EnemySpeed; // не пригодилось
    public int MaxHealth = 100;
    private int _health;

    private NavMeshAgent _agent;
    // private Rigidbody _rigidbody; //возможно для оптимизации следует отказаться от этого и всё переделать 😘 - так и вышло🤓
    private LinkedListNode<Vector3> Target;

    // private bool _targetReached;
    private LinkedList<Vector3> _route;

    void Start()
    {
        _health = MaxHealth;
        
        //навигатор 👉🗺👈
        _route = new LinkedList<Vector3>(GameObject.FindGameObjectsWithTag("WayPointerBase").OrderByDescending(x => x.name)
            .Select(x => x.transform.position)); // для маршрута важен порядок точек.
                                                 // TODO сделать сортировку получше, по нормальному индексу точки в маршуте(🤡)
        Target = _route.First;
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(Target.Value);
    }

    private void Update()
    {
        Move();
    }

    //движение по маршруту (поездка по мешкартам займет 20 минут)
    public void Move()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance) //он говорит инвертировать выражение, чтобы уменьшить гнездование, помогите
        {
            if (Target.Next != null)
            {
                Target = Target.Next;
                _agent.SetDestination(Target.Value);
            }
        }
    }

    // Получение урона
    public void TakeDamage(int Damage) {
        _health -= Damage;

        if (_health <= 0){
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // При столкновение с игроком 
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
