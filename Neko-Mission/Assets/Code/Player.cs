using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovingSpeed;
    public float JumpSpeed;
    public float MaxGroundDist = 0.6f;
    public float Gravity = 5f;
    public int CurrentBalance = 0;
    public float AddBalanceTime;
    
    private Rigidbody _rigidbody;
    private Transform _groundCheckObj;
    private bool _isGrounded;
    private Camera _camera;
    
    private bool _hasJump;
    private float _hasJumpTime;
    private float _jumpBufferTime = 0.5f;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundCheckObj = GameObject.FindGameObjectWithTag("GroundCheck").transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CurrentBalance = 0;
        if (AddBalanceTime > 0)
        {
            InvokeRepeating("AddBalance", AddBalanceTime, AddBalanceTime);
        }
    }

    private void Update()
    {
        if (!_hasJump)
        {
            _hasJump = Input.GetKeyDown(KeyCode.Space);
        }
        else
        {
            _hasJumpTime += Time.deltaTime;
            if (_hasJumpTime > _jumpBufferTime)
            {
                ResetJump();
            }
        }
    }

    void FixedUpdate()
    {
        //Механика передвижения.
        float deltaHor = Input.GetAxis("Horizontal");
        float deltaVer = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * deltaHor + transform.forward * deltaVer;
        if (direction.magnitude > 1.0f)
        {
            direction.Normalize();
        }

        _rigidbody.velocity = direction.magnitude * MovingSpeed * direction + new Vector3(0f, _rigidbody.velocity.y);
        
        //Механика прыжка.
        _isGrounded = Physics.Raycast(_groundCheckObj.transform.position, Vector3.down, MaxGroundDist);
        if (_hasJump && _isGrounded)
        {
            ResetJump();
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = JumpSpeed;
            _rigidbody.velocity = velocity;
        }
        else if (!_isGrounded)
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y -= Gravity * Time.fixedDeltaTime;
            _rigidbody.velocity = velocity;
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
}