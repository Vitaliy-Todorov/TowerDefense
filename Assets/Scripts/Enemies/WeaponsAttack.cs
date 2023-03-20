using System;
using System.Collections;
using UnityEngine;
 
public class WeaponsAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    
    private float _damage;
    private float _timeBetweenShots;
    private float _attackDistance;

    private Transform _player;
    private GameObject _gunpointGO;

    public void Construct(World world, EnemyData enemyData)
    {
        _damage = enemyData.Dameg;
        _timeBetweenShots = enemyData.AttackSpeed;
        _attackDistance = enemyData.AttackDistance;
        
        _player = world.CentralTower.transform;
        _gunpointGO = GetComponentInChildren<GunpointGO>().gameObject;
        
        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        while (_player != null)
        {
            float distanceToPlayer = (transform.position - _player.transform.position).magnitude;

            if (distanceToPlayer > _attackDistance)
            {
                yield return new WaitForSeconds(.1f);
                continue;
            }
                
            Quaternion ganRotetion = _gunpointGO.transform.rotation;
            Vector3 ganPosition = _gunpointGO.transform.position;

            GameObject projectile = Instantiate(_projectilePrefab, ganPosition, ganRotetion);
            projectile.GetComponent<Damage>().UpdateDamage(_damage);
            Destroy(projectile, 1);
            
            yield return new WaitForSeconds(_timeBetweenShots);
        }
    }
}