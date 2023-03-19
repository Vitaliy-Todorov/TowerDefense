using System;
using UnityEngine;

[Serializable]
public class EnemyData : IHealthData
{
    public EEnemyType Type;
    
    public event Action<float> UpdateHealth;
    public event Action<float> UpdateRestoringHealth;
    
    [SerializeField] private float _health;
    [SerializeField] private float _restoringHealth;
    public float Health { get => _health; set => _health = value; }
    public float RestoringHealth { get => _restoringHealth; set => _restoringHealth = value; }
    
    public float SpeedMove;

    public float Dameg;
    public float AttackSpeed;
    public float AttackDistance;
}