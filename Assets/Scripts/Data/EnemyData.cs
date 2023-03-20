using System;
using UnityEngine;

[Serializable]
public class EnemyData : IHealthData
{
    public event Action<float> UpdateHealth;
    public event Action<float> UpdateRestoringHealth;
    
    private EEnemyType _type;
    public EEnemyType Type => _type;

    [SerializeField] private float _health;
    [SerializeField] private float _restoringHealth;
    public float Health { get => _health; set => _health = value; }
    public float RestoringHealth { get => _restoringHealth; set => _restoringHealth = value; }
    
    public float SpeedMove;

    public float Dameg;
    public float AttackSpeed;
    public float AttackDistance;

    private EnemyData(EEnemyType enemyType)
    {
        _type = enemyType;
    }

    public EnemyData Clone()
    {
        EnemyData enemyData = new EnemyData(_type)
        {
            _health = _health,
            _restoringHealth = _restoringHealth,
            Dameg = Dameg,
            AttackSpeed = AttackSpeed,
            AttackDistance = AttackDistance
        };

        return enemyData;
    }
}