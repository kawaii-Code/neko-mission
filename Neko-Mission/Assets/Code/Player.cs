using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovingSpeed;
    public float JumpSpeed;
    public float MaxGroundDist = 0.6f;
    public int CurrentBalance = 0;
    public float AddBalanceTime;
    
    private Rigidbody _rigidbody;
    private Transform _groundCheckObj;
    private bool _isGrounded;
    private float _rotationX = 0;
    private float _rotationY = 0;
    private Camera _camera;
    
    private bool _hasJump;
    private float _hasJumpTime = 0.5f;
    private float _bufferTime = 0.5f;
    
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
            if (_hasJumpTime > _bufferTime)
            {
                ResetJump();
            }
        }
    }

    void FixedUpdate()
    {
        //Механика передвижения.
        float deltaHor = Input.GetAxis("Horizontal") * MovingSpeed;
        float deltaVer = Input.GetAxis("Vertical") * MovingSpeed;
        Vector3 newpositionvec = transform.forward * (deltaVer * MovingSpeed * Time.fixedDeltaTime);
        Vector3 newpositionhor = transform.right * (deltaHor * MovingSpeed * Time.fixedDeltaTime);

        _rigidbody.velocity = newpositionhor + newpositionvec + new Vector3(0f, _rigidbody.velocity.y);
        
        //Механика прыжка.
        _isGrounded = Physics.Raycast(_groundCheckObj.transform.position, Vector3.down, MaxGroundDist);
        if (_hasJump && _isGrounded)
        {
            ResetJump();
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = JumpSpeed;
            _rigidbody.velocity = velocity;
            //_rigidbody.AddForce(Vector3.up * (100 * JumpSpeed));
        }
        else if (!_isGrounded)
        {
            _rigidbody.AddForce(Vector3.down * 100);
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