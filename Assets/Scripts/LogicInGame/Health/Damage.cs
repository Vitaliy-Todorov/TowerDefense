using Abilities;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private float _damage;

    public void UpdateDamage(float damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        health?.TakeDamage(_damage);
    }
}
