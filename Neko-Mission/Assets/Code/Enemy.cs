using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public int MaxHealth = 100;
    private int _health;

    private NavMeshAgent _agent;
    // private Rigidbody _rigidbody; //возможно для оптимизации следует отказаться от этого и всё переделать 😘 - так и вышло🤓
    private LinkedListNode<Vector3> _target;
    private LinkedList<Vector3> _route;

    void Start()
    {
        _health = MaxHealth;
        
        //навигатор 👉🗺👈
        _route = new LinkedList<Vector3>(GameObject.FindGameObjectsWithTag("WayPointerBase").OrderByDescending(x => x.name)
            .Select(x => x.transform.position)); // для маршрута важен порядок точек - это можно попробовать исправить
        // TODO сделать сортировку получше, по нормальному индексу точки в маршуте(🤡)
        
        _target = _route.First;
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_target.Value);
        _agent.speed = Speed;
    }

    private void Update()
    {
        Move();
    }
    
    
    // TODO FIX -> по неведомой мне сейчас причине, некоторые враги начинают со второй точки в маршруте😠
    
    //движение по маршруту (поездка по мешкартам займет 20 минут)
    public void Move()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance) //он говорит инвертировать выражение, чтобы уменьшить гнездование, помогите
        {
            if (_target.Next != null)
            {
                _target = _target.Next;
                _agent.SetDestination(_target.Value);
            }
        }
    }

    // Получение урона
    public void TakeDamage(int damage) {
        _health -= damage;

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
