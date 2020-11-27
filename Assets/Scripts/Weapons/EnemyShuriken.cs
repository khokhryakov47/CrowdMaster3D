using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShuriken : Shuriken, IDamageable
{
    [SerializeField] private float _speedRate;

    private bool _flipped;

    private void OnEnable()
    {
        _flipped = false;
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            if (player.ApplyDamage(Body, Speed))
                Destroy(gameObject);
        }
        if (collider.TryGetComponent(out IDamageable damageable))
        {
            if (_flipped == false && damageable is Enemy)
                return;

            if (damageable.ApplyDamage(Body, Speed))
                Destroy(gameObject);
        }
    }

    public bool ApplyDamage(Rigidbody body, float damage)
    {
        if (_flipped)
            return false;

        Direction = body.transform.forward.ToPlane();
        Speed *= _speedRate;
        _flipped = true;
        return true;
    }

    protected override void Move()
    {
        Body.velocity = Direction * Speed;
    }
}
