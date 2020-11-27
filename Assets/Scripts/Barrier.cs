using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Barrier : MonoBehaviour, IDamageable
{
    [SerializeField] private float _forceRate;

    private Rigidbody _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    public bool ApplyDamage(Rigidbody attachedBody, float force)
    {
        if (_body.velocity.magnitude > force)
            return false;

        Vector3 forwardDirection = attachedBody.transform.forward.ToPlane();
        _body.AddForce(forwardDirection.normalized * force * _forceRate, ForceMode.Force);
        return true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IDamageable damageable))
            damageable.ApplyDamage(_body, _body.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            damageable.ApplyDamage(_body, _body.velocity.magnitude);
    }
}
