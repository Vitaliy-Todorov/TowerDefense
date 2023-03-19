using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody _rigidbody;

    private Transform _target;
    private bool _move;

    public void Construct(Transform target, float speed)
    {
        _speed = speed;
        _rigidbody = GetComponent<Rigidbody>();
        _target = target;
        StartMove();
    }

    public void Construct(Transform target)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = target;
        StartMove();
    }

    void Update()
    {
        if(_move)
            Move();
    }
        
    private void Move()
    {
        if (_target == null)
        {
            return;
        }
                
        Vector3 direction = _target.position - transform.position;
        direction.y = transform.position.y;
        
        /*Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);*/
        //transform.rotation = Quaternion.LookRotation(direction);
        
        _rigidbody.velocity = direction.normalized * _speed;
    }

    public void StartMove() =>
        _move = true;

    public void StopMove() =>
        _move = false;
}