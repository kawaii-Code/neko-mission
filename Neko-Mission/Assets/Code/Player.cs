using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovingSpeed;
    public float RotationSpeed;
    public float JumpSpeed;
    public float Gravity;
    private float _verticalVelocity;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
       //if (move.magnitude>0.1f)
       //{
           // Quaternion rotation = Quaternion.LookRotation(move);
           // rotation.x =0;
           // rotation.z =0;
            //transform.rotation = Quaternion.Lerp(transform.rotation,rotation,RotationSpeed*Time.deltaTime);
        // }
       _rigidbody.velocity=move*MovingSpeed;
       //bool hasJump = Input.GetKeyDown(KeyCode.Space);
       //_verticalVelocity +=Gravity*Time.deltaTime;
       //if (hasJump)
       //{
            //Vector3 position = transform.position;
            //position.y+=JumpSpeed*Time.deltaTime;
            //transform.position = position;
      // }
       //Vector3 position = transform.position;
       //position.y+=_verticalVelocity*Time.deltaTime;
       //transform.position = position;

    }
}
