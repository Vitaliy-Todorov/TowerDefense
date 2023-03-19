using System;
using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float _maxHealth;
        [SerializeField]
        private float _currentHealth;
        [SerializeField]
        private HealthBar healthBar;

        private float _timeRestoringHealth = 1;
        private float _restoringHealth = 1;

        public event Action ExecuteAtDeath;
        
        public delegate void DDamage(float damage);
        public event DDamage DoDamage;

        private void Awake()
        {
            healthBar?.SetMaxHealth(_maxHealth);
            healthBar?.SetHealth(_currentHealth);

            StartCoroutine(RestoringHealth());
        }

        public void Construct(IHealthData healthData)
        {
            _maxHealth = healthData.Health;
            _currentHealth = healthData.Health;
            _restoringHealth = healthData.RestoringHealth;
            
            healthData.UpdateHealth += UpdateHealth;
            healthData.UpdateRestoringHealth += UpdateRestoringHealth;
            
            healthBar?.SetMaxHealth(_maxHealth);
            healthBar?.SetHealth(_currentHealth);
        }

        public void UpdateHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            
            healthBar?.SetMaxHealth(_maxHealth);
        }

        public void UpdateRestoringHealth(float restoringHealth)
        {
            _restoringHealth = restoringHealth;
        }

        private IEnumerator RestoringHealth()
        {
            while (true)
            {
                if (_currentHealth < _maxHealth)
                {
                    _currentHealth += _restoringHealth;
                    healthBar?.SetHealth(_currentHealth);
                }
                
                yield return new WaitForSeconds(_timeRestoringHealth);
            }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            DoDamage?.Invoke(damage);
            healthBar?.SetHealth(_currentHealth);

            if (_currentHealth <= 0)
            {
                ExecuteAtDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}