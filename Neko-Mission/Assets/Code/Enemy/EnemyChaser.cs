using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyChaser : MonoBehaviour
{
    public float Speed;
    public int MaxHealth = 100;
    public Slider HealthBar;
    public Animator Animator;

    private int _health;
    public bool IsDead;
    private NavMeshAgent _agent;
    // private Rigidbody _rigidbody; //возможно для оптимизации следует отказаться от этого и всё переделать 😘 - так и вышло🤓
    private LinkedListNode<Vector3> _target;
    private LinkedList<Vector3> _route;

    public event Action<EnemyChaser> Dead;

    public float ChaseDistance = 5f;
    public float ReturnDistance = 10f;
    public float ChaseSpeed = 3f;

    private bool _isChasing;
    private Vector3 _playerLastPosition;
    private float _chaseTimer;
    private Vector3 _chaseStartPoint;
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

    void Update()
    {
        if (IsDead)
            return;
        // Проверка дистанции до игрока
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        var distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        if (distanceToPlayer < ChaseDistance)
        {
            _isChasing = true;
        }
        if (_isChasing)
        {
            if (_chaseStartPoint == Vector3.zero)
            {
                _chaseStartPoint = transform.position;
            }
            ChasePlayer();
        }
        else
        {
            if (_chaseStartPoint != Vector3.zero)
            {
                ReturnToChaseStart();
            }
            else
            {
                Move();
            }
        }
        if (distanceToPlayer < ChaseDistance && !_isChasing)
        {
            _isChasing = true;
        }
    }
    // возвращает на позицию с начала преследования
    private void ReturnToChaseStart()
    {
        if (Vector3.Distance(transform.position, _chaseStartPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _chaseStartPoint, Speed * Time.deltaTime);
            transform.LookAt(_chaseStartPoint);
        }
        else
        {
            _chaseStartPoint = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            _agent.enabled = true;
            _agent.SetDestination(_target.Value);
        }
    }
    private void ChasePlayer()
    {
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        var distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        GetComponent<Rigidbody>().useGravity = true;
        _agent.enabled = false;

        if (distanceToPlayer < ChaseDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, ChaseSpeed * Time.deltaTime);
            transform.LookAt(new Vector3(playerPosition.x, transform.position.y, playerPosition.z));
        }
        else if (distanceToPlayer >= ReturnDistance)
        {
            _isChasing = false;
        }
        else
        {
            _chaseTimer += Time.deltaTime;

            if (_chaseTimer > 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, ChaseSpeed * Time.deltaTime);
                transform.LookAt(new Vector3(playerPosition.x, transform.position.y, playerPosition.z));
                _chaseTimer = 0f;
            }
            else
            {
                _isChasing = false;
            }
        }
    }
    // TODO FIX -> по неведомой мне сейчас причине, некоторые враги начинают со второй точки в маршруте😠
    //движение по маршруту (поездка по мешкартам займет 20 минут)
    // движение по маршруту (поездка по мешкартам займет 20 минут)
    public void Move()
    {
        _agent.enabled = true;
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
        IsDead = true;
        Sounds.Play("bone-crush");
        _agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().Sleep();
        GetComponentInChildren<Canvas>().enabled = false;
        Dead?.Invoke(this);
        Animator.Play("Death");
        Destroy(gameObject, 5);
    }

    // Получение урона
    public void TakeDamage(int damage)
    {
        _health -= damage;
        UpdateHealthbar();

        if (_health <= 0)
        {
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