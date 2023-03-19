using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody _rigidbody;

    private Transform _target;
    private bool _move;

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
            Destroy(gameObject);
            return;
        }
        
        Vector3 direction = _target.position - transform.position;
        _rigidbody.velocity = transform.forward * _speed;
                
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed * 10 / direction.magnitude);
    }

    public void StartMove() =>
        _move = true;

    public void StopMove() =>
        _move = false;
}
