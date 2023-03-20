using System;
using UnityEngine;

[Serializable]
public class TowerData : IHealthData
{
    public event Action<float> UpdateHealth;
    public event Action<float> UpdateRestoringHealth;
    public event Action<float> UpdateDamage;
    public event Action<float> UpdateAttackRadius;
    public event Action<float> UpdateShootingSpeed;

    [SerializeField] private ETowerType _towerType;
    [SerializeField] private float _health;
    [SerializeField] private float _restoringHealth;
    [SerializeField] private float _dameg;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private float _attackRadius;

    public ETowerType TowerType => _towerType;

    public float Health
    {
        get => _health;
        set
        {
            UpdateHealth?.Invoke(value);
            _health = value;
        }
    }

    public float RestoringHealth 
    {
        get => _restoringHealth;
        set
        {
            UpdateRestoringHealth?.Invoke(value);
            _restoringHealth = value;
        }
    }

    public float Dameg
    {
        get => _dameg;
        set
        {
            UpdateDamage?.Invoke(value);
            _dameg = value;
        }
    }

    public float AttackRadius
    {
        get => _attackRadius;
        set
        {
            UpdateAttackRadius?.Invoke(value);
            _attackRadius = value;
        }
    }

    public float ShootingSpeed
    {
        get => _shootingSpeed;
        set
        {
            UpdateShootingSpeed?.Invoke(value);
            _shootingSpeed = value;
        }
    }

    private TowerData(ETowerType towerType)
    {
        _towerType = towerType;
    }

    public TowerData Clone()
    {
        TowerData towerData = new TowerData(_towerType)
        {
            _health = _health,
            _restoringHealth = _restoringHealth,
            _dameg = _dameg,
            _shootingSpeed = _shootingSpeed,
            _attackRadius = _attackRadius
        };

        return towerData;
    }
}