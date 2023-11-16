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
    public LayerMask GroundMask;
    public float MaxGroundDist = 0.6f;
    
    private Rigidbody _rigidbody;
    private Transform _groundCheckObj;
    private bool _isGrounded;
    private float _rotationX = 0;
    private float _rotationY = 0;
    private Camera _camera;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundCheckObj = GameObject.FindGameObjectWithTag("GroundCheck").transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Механика передвижения.
        float deltaHor = Input.GetAxis("Horizontal") * MovingSpeed;
        float deltaVer = Input.GetAxis("Vertical") * MovingSpeed;
        Vector3 newpositionvec = transform.forward * (deltaVer * MovingSpeed * Time.deltaTime);
        Vector3 newpositionhor = transform.right * (deltaHor * MovingSpeed * Time.deltaTime);

        _rigidbody.velocity = newpositionhor + newpositionvec + new Vector3(0f, _rigidbody.velocity.y);
        //_rigidbody.AddForce(newpositionvec + newpositionhor, ForceMode.Force);
        //_rigidbody.MovePosition(transform.position + newpositionvec + newpositionhor);
        //Механика прыжка.
        _isGrounded = Physics.Raycast(_groundCheckObj.transform.position, Vector3.down, MaxGroundDist);
        bool hasJump = Input.GetKeyDown(KeyCode.Space);
        if (hasJump && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * (100 * JumpSpeed));
        }
        else if (!_isGrounded)
        {
            _rigidbody.AddForce(Vector3.down * 100);
        }

        //Вид от 1-го лица.
        _rotationX -= Input.GetAxis("Mouse Y") * RotationSpeedVer;
        _rotationX = Mathf.Clamp(_rotationX, MinVer, MaxVer);
        float delta = Input.GetAxis("Mouse X") * RotationSpeedHor;
        _rotationY = transform.localEulerAngles.y + delta;
        
        Camera.transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        transform.rotation = Quaternion.Euler(0, _rotationY, 0);
    }
}