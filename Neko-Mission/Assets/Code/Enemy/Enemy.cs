using System;
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

    // Получение урона
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Sounds.Play("duck");
            Dead?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            Sounds.Play("click2");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // При столкновение с игроком 
        if (other.gameObject.CompareTag("Player"))
        {
            var Pl = other.gameObject.GetComponent<Player>();
            LoseMenu.Show();
            Pl.CurrentHealth = Pl.StartingHealth;
            //Destroy(other.gameObject);
        }
    }
}
