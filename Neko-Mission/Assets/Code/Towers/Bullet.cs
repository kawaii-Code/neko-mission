using System.Collections;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Target; // Targe || Target pos 
    public Vector3 TargetPos;
    public int Damage;
    public float Speed;
    public bool SlowdownIsActivated;

    private Enemy _actionTarget;
    private bool TypeMove = true; //  true - в обьект, false - в позицию
    private Vector3 _speed; // в позицию 

    private float _time;
    void Start()
    {
        _time = 0;
        if (!Target) TypeMove = false; // Определение тип 

        if (!TypeMove)
        {
            _speed.x = TargetPos.x - transform.position.x;
            _speed.y = TargetPos.y - transform.position.y;
            _speed.z = TargetPos.z - transform.position.z;
        }
    }

    void Update()
    {
        if (_time > 3) Destroy(this.gameObject);
        if (TypeMove)
        {
            if (!Target)
            {
                Destroy(gameObject); // Если другая башня убила врага, а пуля осталась 
                return;
            }

            transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, Speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(TargetPos - transform.position);
            transform.position += _speed * Speed * Time.deltaTime;
        }

        _time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other)
        {
            Destroy(this.gameObject);

            if (other.tag == "Enemy")
            {
                var damageEnemy = other.GetComponent<Enemy>();
                damageEnemy.TakeDamage(Damage);

                if (SlowdownIsActivated)
                {
                    damageEnemy.GetSlowedDown();
                }

            }
        }
    }

}
