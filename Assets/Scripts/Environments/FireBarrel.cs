using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FireBarrel : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _detonationValue;
    [SerializeField] private float _damageValue;

    private Rigidbody _body;

    private void OnDrawGizmos()
    {
        if (Selection.activeGameObject != transform.gameObject)
            return;

            Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    public bool ApplyDamage(Rigidbody body, float force)
    {
        if (force < _detonationValue)
            return false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.Equals(gameObject))
                continue;

            if (collider.TryGetComponent(out IDamageable damageable))
                damageable.ApplyDamage(_body, _damageValue);
        }

        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        return true;
    }
}
