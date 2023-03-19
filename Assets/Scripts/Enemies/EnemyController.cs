using Abilities;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private World _world;

    [SerializeField] 
    private EEnemyType _type;
    public EEnemyType Type => _type;
    
    [SerializeField] 
    private Animator _animator;
    [SerializeField] 
    private GameObject _coinPrefab;

    private MoveTo _move;

    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Idle = Animator.StringToHash("Idle");

    public void Construct(World world, EnemyData enemyData)
    {
        _world = world;

        _move = GetComponent<MoveTo>();
        _move.Construct(_world.CentralTower.transform, enemyData.SpeedMove);
        
        Health health = GetComponent<Health>();
        health.ExecuteAtDeath += Death;
        health.Construct(enemyData);
        
        GetComponent<WeaponsAttack>()
            .Construct(_world, enemyData);
    }

    private void Death()
    {
        Instantiate(_coinPrefab, transform.position, _coinPrefab.transform.rotation)
            .GetComponent<MoveTo>()
            .Construct(_world.Counter);
    }

    private void StartMove()
    {
        _animator?.ResetTrigger(Move);
        _move?.StopMove();
        _animator?.SetTrigger(Idle);
    }

    private void StopMove()
    {
        _animator?.ResetTrigger(Idle);
        _move?.StartMove();
        _animator?.SetTrigger(Move);
    }
}