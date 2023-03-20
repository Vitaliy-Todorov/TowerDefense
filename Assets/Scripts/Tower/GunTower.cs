using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTower : MonoBehaviour
{
    [SerializeField] 
    private GameObject _projectilePrefab;

    private World _world;

    private float _damage;
    private float _attackRadius;
    private float _timeBetweenShots = 2;

    private GameObject _gunpointGO;
    private bool _stop;
    private Coroutine _shot;

    public void Construct(World world, TowerData towerData)
    {
        _world = world;
        
        _damage = towerData.Dameg;
        towerData.UpdateDamage += UpdateDameg;
        
        _attackRadius = towerData.AttackRadius;
        towerData.UpdateAttackRadius += UpdateAttackRadius;
        
        _timeBetweenShots = towerData.ShootingSpeed;
        towerData.UpdateShootingSpeed += UpdateShootingSpeed;
        
        _gunpointGO = GetComponentInChildren<GunpointGO>().gameObject;
        _shot = StartCoroutine(Shot());
    }

    private void UpdateDameg(float damage) => 
        _damage = damage;

    private void UpdateAttackRadius(float attackRadius) => 
        _attackRadius = attackRadius;

    private void UpdateShootingSpeed(float timeBetweenShots)
    {
        if(timeBetweenShots >= 0)
            _timeBetweenShots = timeBetweenShots;
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            Quaternion ganRotetion = _gunpointGO.transform.rotation;
            Vector3 ganPosition = _gunpointGO.transform.position;

            Transform nearestEnemy = NearestEnemy();

            if (nearestEnemy != null)
            {
                GameObject projectile = Instantiate(_projectilePrefab, ganPosition, ganRotetion);

                projectile.GetComponent<Damage>()
                    .UpdateDamage(_damage);
                projectile.GetComponent<ProjectileMovement>()
                    .Construct(nearestEnemy);
            }
            
            yield return new WaitForSeconds(_timeBetweenShots);
        }
    }

    public void StartShot() => 
        _shot = StartCoroutine(Shot());

    public void StopShot() => 
        StopCoroutine(_shot);

    private Transform NearestEnemy()
    {
        List<GameObject> enemies = _world.EnemiesSpawner.Enemies;

        Transform nearestEnemy = null;
        float minDistanceToEnemy = float.MaxValue;
        float distanceToEnemy;

        foreach (GameObject enemy in enemies)
        {
            distanceToEnemy = (transform.parent.position - enemy.transform.position).magnitude;

            if (distanceToEnemy < minDistanceToEnemy && distanceToEnemy < _attackRadius)
            {
                minDistanceToEnemy = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
}