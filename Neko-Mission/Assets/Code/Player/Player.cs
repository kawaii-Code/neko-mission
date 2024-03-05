using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovingSpeed;
    public float JumpSpeed;
    public float MaxGroundDist = 0.6f;
    public float WaterDamageCooldown = 0.5f;
    
    public float Gravity = 5f;
    public int StartingBalance = 10;
    public int CurrentBalance = 0;
    public int CurrentHealth = 100;
    public int StartingHealth = 100;
    public int DmgWater = 20;
    public float AddBalanceTime;

    private CharacterController _controller;
    private Transform _groundCheckObj;
    private bool _isGrounded;
    private PlayerCamera _playerCamera;

    private Vector3 _velocity;
    
    private bool _hasJump;
    private float _hasJumpTime;
    private float _jumpBufferTime = 0.5f;
    private float _waterDamageCooldownCurrent;
    private bool _inWater;

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

        _velocity = direction * (direction.magnitude * MovingSpeed) + Vector3.up * _velocity.y;
        _controller.Move(_velocity * Time.deltaTime);

        //Механика прыжка.
        _isGrounded = Physics.Raycast(_groundCheckObj.transform.position, Vector3.down, MaxGroundDist);
        if (_hasJump)
        {
            if (_isGrounded)
            {
                ResetJump();
                _velocity.y = JumpSpeed;
            }

            _hasJumpTime += Time.deltaTime;
            if (_hasJumpTime > _jumpBufferTime)
            {
                ResetJump();
            }
        }
        else
        {
            _hasJump = Input.GetKeyDown(KeyCode.Space);
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
        _hasJump = false;
        _hasJumpTime = 0.0f;
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
            CurrentHealth -= DmgWater;
            if(CurrentHealth <=0)
            {
                Sounds.Play("heartbeat");
                LoseMenu.Show();
            }
            _waterDamageCooldownCurrent = 0.0f;
        }

        _waterDamageCooldownCurrent += Time.deltaTime;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Water")
        {
            _inWater = true;
        }
        else
        {
            _inWater = false;
            _waterDamageCooldownCurrent = 0.0f;
        }
    }
}