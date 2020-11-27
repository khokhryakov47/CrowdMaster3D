using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShuriken : Shuriken
{
    private void OnEnable()
    {
        Direction = Vector3.zero;
    }

    private void Start()
    {
        Body.AddForce(Direction * 20f, ForceMode.Impulse);
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(Body, 20f);
            Destroy(gameObject);
        }
    }

    protected override void Move()
    {
        
    }
}
