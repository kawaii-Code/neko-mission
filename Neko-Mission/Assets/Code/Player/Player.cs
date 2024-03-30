using System;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovingSpeed;
    public float JumpSpeed;
    public float BunnyHopAcceleration;
    public float MaxGroundDist = 0.6f;
    public float WaterDamageCooldown = 0.5f;
    public int MaxHopAccelerationCount = 3;
    
    public float Gravity = 5f;
    public int StartingBalance = 10;
    public int CurrentBalance = 0;
    public int CurrentHealth = 100;
    public int StartingHealth = 100;
    public int DmgWater = 20;
    public float AddBalanceTime;
    public LayerMask WaterLayer;

    private CharacterController _controller;
    private Transform _groundCheckObj;
    private bool _isGrounded;
    private PlayerCamera _playerCamera;

    private Vector3 _velocity;
    
    private bool _isInJump;
    private float _inJumpTime;
    private bool _needHopAcceleration = false;
    private int _hopAccelerationCount = 0;
    private float _jumpBufferTime = 0.5f;
    private float _waterDamageCooldownCurrent;
    private bool _inWater;
    private float _speedAccelerationModifier = 1;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _groundCheckObj = GameObject.FindGameObjectWithTag("GroundCheck").transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CurrentHealth = StartingHealth;
        CurrentBalance = StartingBalance;
        if (AddBalanceTime > 0)
        {
            InvokeRepeating("AddBalance", AddBalanceTime, AddBalanceTime);
        }
    }

    private void Update()
    {
        float deltaHor = Input.GetAxis("Horizontal");
        float deltaVer = Input.GetAxis("Vertical");
        
        Vector3 direction = transform.right * deltaHor + transform.forward * deltaVer;
        if (direction.magnitude > 1.0f)
        {
            direction.Normalize();
        }

        _velocity = direction * (direction.magnitude * MovingSpeed * _speedAccelerationModifier) + Vector3.up * _velocity.y;
        _controller.Move(_velocity * Time.deltaTime);

        //Механика прыжка.
        _isGrounded = Physics.Raycast(_groundCheckObj.transform.position, Vector3.down, MaxGroundDist);
        if (_isInJump)
        {
            if (_isGrounded)
            {
                if (!_inWater && Physics.CheckSphere(transform.position, 0.8f, WaterLayer))
                {
                    _inWater = true;
                    _waterDamageCooldownCurrent = WaterDamageCooldown;
                }
                else if (_inWater && !Physics.CheckSphere(transform.position, 0.8f, WaterLayer))
                {
                    _inWater = false;
                    _waterDamageCooldownCurrent = 0.0f;
                }

                ResetJump();
                _velocity.y = JumpSpeed;
                if (_needHopAcceleration)
                {
                    if (_hopAccelerationCount < MaxHopAccelerationCount)
                    {
                        _speedAccelerationModifier *= BunnyHopAcceleration;
                        ++_hopAccelerationCount;
                    }
                    _needHopAcceleration = false;
                }
                else
                {
                    _speedAccelerationModifier = 1;
                    _hopAccelerationCount = 0;
                }
            }

            _inJumpTime += Time.deltaTime;
            if (_inJumpTime > _jumpBufferTime)
            {
                ResetJump();
            }
        }
        else
        {
            _isInJump = Input.GetKeyDown(KeyCode.Space);
            if (!_isGrounded)
            {
                _needHopAcceleration = true;
            }
            else
            {
                _needHopAcceleration = false;
                _hopAccelerationCount = 0;
                _speedAccelerationModifier = 1;
            }
        }

        if (!_isGrounded)
        {
            _velocity.y -= Gravity * Time.deltaTime;
        }

        if (_inWater)
        {
            GetDamageFromWater();
        }
    }

    private void ResetJump()
    {
        _isInJump = false;
        _inJumpTime = 0.0f;
    }

    //Добавление денег 
    private void AddBalance()
    {
        CurrentBalance++;
    }

    private void GetDamageFromWater()
    {
        if (_waterDamageCooldownCurrent > WaterDamageCooldown)
        {
            TakeDamage(DmgWater);
            _waterDamageCooldownCurrent = 0.0f;
        }

        _waterDamageCooldownCurrent += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        Sounds.Play("cat-quick-meow");
        CurrentHealth -= damage;
        
        if(CurrentHealth <=0)
        {
            Sounds.Play("heartbeat");
            LoseMenu.Show();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Water"))
        {
            if (!_inWater)
            {
                _waterDamageCooldownCurrent = WaterDamageCooldown;
            }
            _inWater = true;
        }
        else
        {
            _inWater = false;
            _waterDamageCooldownCurrent = 0.0f;
        }
    }
}