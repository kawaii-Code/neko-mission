using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Camera;
    public float RotationSpeedHor = 5.0f;
    public float RotationSpeedVer = 5.0f;
    public float MinVer = 45.0f;
    public float MaxVer = 45.0f;
    public float MovingSpeed;
    public float JumpSpeed;
<<<<<<< Updated upstream
    private float _maxGroundDist = 0.6f;
=======
    public LayerMask GroundMask;
    public float MaxGroundDist = 0.6f;
    public int CurrentBalance = 0;
    public float AddBalanceTime;

>>>>>>> Stashed changes
    private Rigidbody _rigidbody;
    private Transform _groundCheckObj;
    private bool _isGrounded;
    private float _rotationX = 0;
    private Camera _camera;
<<<<<<< Updated upstream
    public long CurrentBalance = 0;
    public int AddBalanceTime;
=======

>>>>>>> Stashed changes
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

    void Update()
    {
        //Механика передвижения.
        float deltaHor = Input.GetAxis("Horizontal") * MovingSpeed;
        float deltaVer = Input.GetAxis("Vertical") * MovingSpeed;
        Vector3 newpositionvec = transform.forward * deltaVer * MovingSpeed * Time.deltaTime;
        Vector3 newpositionhor = transform.right * deltaHor * MovingSpeed * Time.deltaTime;
        _rigidbody.MovePosition(transform.position + newpositionvec + newpositionhor);
        //Механика прыжка.
        _isGrounded = Physics.Raycast(_groundCheckObj.transform.position, Vector3.down, _maxGroundDist);
        bool hasJump = Input.GetKeyDown(KeyCode.Space);
        if (hasJump && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * 100 * JumpSpeed);
        }
        else if (!_isGrounded)
        {
            _rigidbody.AddForce(Vector3.down * 100);
        }

        //Вид от 1-го лица.
        _rotationX -= Input.GetAxis("Mouse Y") * RotationSpeedVer;
        _rotationX = Mathf.Clamp(_rotationX, MinVer, MaxVer);
        float delta = Input.GetAxis("Mouse X") * RotationSpeedHor;
<<<<<<< Updated upstream
        float _rotationY = transform.localEulerAngles.y + delta;
        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
=======
        _rotationY = transform.localEulerAngles.y + delta;

        Camera.transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        transform.rotation = Quaternion.Euler(0, _rotationY, 0);
>>>>>>> Stashed changes
    }

    //Добавление денег 
    private void AddBalance()
    {
        CurrentBalance++;
    }
}
