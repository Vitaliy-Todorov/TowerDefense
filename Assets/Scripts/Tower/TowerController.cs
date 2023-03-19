using Abilities;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] 
    private ETowerType _towerType;
    public ETowerType TowerType => _towerType;

    [SerializeField] 
    private GunTower _gun;
    [SerializeField] 
    private Animator _animator;

    private World _world;

    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Idle = Animator.StringToHash("Idle");
    
    public void Construct(World world, TowerData towerData)
    {
        _world = world;
        
        Health health = GetComponent<Health>();
        health.Construct(towerData);
        //ConstructHealth();

        _gun.Construct(_world, towerData);
    }

    /*private void ConstructHealth()
    {
        Health health = GetComponent<Health>();
        health.Construct(_world.GeneralData.TowersDatas.Health, _world.GeneralData.TowersDatas.RestoringHealth);
        _world.GeneralData.TowersDatas.UpdateHealth += health.UpdateHealth;
        _world.GeneralData.TowersDatas.UpdateRestoringHealth += health.UpdateRestoringHealth;
    }*/

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