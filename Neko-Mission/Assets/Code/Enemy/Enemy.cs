using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public int MaxHealth = 100;
    public Slider HealthBar;
    private int _health;


    private NavMeshAgent _agent;
    // private Rigidbody _rigidbody; //возможно для оптимизации следует отказаться от этого и всё переделать 😘 - так и вышло🤓
    private LinkedListNode<Vector3> _target;
    private LinkedList<Vector3> _route;

    public event Action<Enemy> Dead; 

    void Start()
    {
        _health = MaxHealth;

        //навигатор 👉🗺👈
        _route = new LinkedList<Vector3>(GameObject.FindGameObjectsWithTag("WayPointerBase").OrderBy(x => x.name)
            .Select(x => x.transform.position)); // для маршрута важен порядок точек - это можно попробовать исправить
                                                 // TODO сделать сортировку получше, по нормальному индексу точки в маршуте(🤡)

        _target = _route.First;
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_target.Value);
        _agent.speed = Speed;
        UpdateHealthbar();
    }

    private void Update()
    {
        Move();
    }

    // TODO FIX -> по неведомой мне сейчас причине, некоторые враги начинают со второй точки в маршруте😠
    //движение по маршруту (поездка по мешкартам займет 20 минут)
    // движение по маршруту (поездка по мешкартам займет 20 минут)
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

    public void Die()
    {
        Dead?.Invoke(this);
        Destroy(gameObject);
    }

    // Получение урона
    public void TakeDamage(int damage)
    {
        _health -= damage;
        UpdateHealthbar();

        if (_health <= 0)
        {
            Sounds.Play("duck");
            Die();
        }
        else
        {
            Sounds.Play("click2");
        }
    }

    private void UpdateHealthbar()
    {
        HealthBar.value = (float)_health / MaxHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        // При столкновение с игроком 
        if (other.gameObject.CompareTag("Player"))
        {
            var Pl = other.gameObject.GetComponent<Player>();
            Pl.TakeDamage(25);
        }
    }
}