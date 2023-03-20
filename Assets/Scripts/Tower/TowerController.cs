using Abilities;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerController : MonoBehaviour
{
    [SerializeField] 
    private ETowerType _towerType;
    public ETowerType TowerType => _towerType;

    [SerializeField] 
    private GunTower _gun;
    [SerializeField] 
    private Animator _animator;
    [SerializeField] 
    private TowerUI _towerUI;

    private World _world;

    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Idle = Animator.StringToHash("Idle");
    
    public void Construct(World world, TowerData towerData)
    {
        _world = world;
        
        Health health = GetComponent<Health>();
        health.Construct(towerData);

        _gun.Construct(_world, towerData);
        _towerUI.Construct(towerData);
    }

    private void StartMove()
    {
        _animator.ResetTrigger(Idle);
        _gun.StopShot();
        _animator.SetTrigger(Move);
    }

    private void StopMove()
    {
        _animator.ResetTrigger(Move);
        _gun.StartShot();
        _animator.SetTrigger(Idle);
    }
}