using System;
using System.Collections;
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
    public Animator Animator;

    private int _health;
    public bool IsDead;
    private NavMeshAgent _agent;
    // private Rigidbody _rigidbody; //возможно для оптимизации следует отказаться от этого и всё переделать 😘 - так и вышло🤓
    private LinkedListNode<Vector3> _target;
    private LinkedList<Vector3> _route;

    const int ChaseWalkableAreaMask = 1 << 3;

    public event Action<Enemy> Dead;

    public bool IsChaser = false;
    public float ChaseDistance = 5f;
    public float ReturnDistance = 10f;

    private bool _isChasing;
    private Vector3 _playerLastPosition;
    private float _chaseTimer;
    private Vector3 _chaseStartPoint;
    private Transform _player;

    void Start()
    {
        _health = MaxHealth;

        //навигатор 👉🗺👈
        _route = new LinkedList<Vector3>(GameObject.FindGameObjectsWithTag("WayPointerBase").OrderBy(x => x.name)
            .Select(x => x.transform.position)); // для маршрута важен порядок точек - это можно попробовать исправить
                                                 // TODO сделать сортировку получше, по нормальному индексу точки в маршуте(🤡)

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _target = _route.First;
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_target.Value);
        _agent.speed = Speed;
        UpdateHealthbar();
    }

    private void Update()
    {
        if (IsDead)
            return;
        if (IsChaser)
        {
            // Проверка дистанции до игрока
            var playerPosition = _player.position;
            var distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
            if (!_isChasing && distanceToPlayer < ChaseDistance)
            {
                _isChasing = true;
                if (_chaseStartPoint == Vector3.zero)
                {
                    _chaseStartPoint = transform.position;
                }
            }

            if (_isChasing)
            {
                _agent.areaMask |= ChaseWalkableAreaMask;
                ChasePlayer(distanceToPlayer);
            }
            else
            {
                if (_chaseStartPoint != Vector3.zero)
                {
                    ReturnToChaseStart();
                }
                else
                {
                    _agent.areaMask &= ~ChaseWalkableAreaMask;
                    Move();
                }
            }
        }
        else
            Move();
    }
    // возвращает на позицию с начала преследования
    private void ReturnToChaseStart()
    {
        var distanceToStartPoint = Vector3.Distance(transform.position, _chaseStartPoint);
        if (distanceToStartPoint > 2f)
        {
            _agent.SetDestination(_chaseStartPoint);
        }
        else
        {
            _chaseStartPoint = Vector3.zero;
            _agent.SetDestination(_target.Value);
        }
    }
    private void ChasePlayer(float distanceToPlayer)
    {
        var playerPosition = _player.position;

        if (distanceToPlayer < ChaseDistance)
        {
            _agent.SetDestination(playerPosition);
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
                _agent.SetDestination(playerPosition);
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
            Sounds.PlayAt("click2", transform.position);
        }
    }

    public IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(3f);
        _agent.speed = Speed;
    }


    public void GetSlowedDown()
    {
        StopAllCoroutines();

        if (_health <= 0)
            return;

        _agent.speed = Speed - 4;
        StartCoroutine("ResetSpeed");
    }

    private void UpdateHealthbar()
    {
        HealthBar.value = (float)_health / MaxHealth;
    }

    private void OnDrawGizmos()
    {
        if (IsChaser)
        {
            Gizmos.DrawWireSphere(transform.position, ChaseDistance);
        }
    }
}
