using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BrokenState : EnemyState
{
    private Rigidbody _body;

    public event UnityAction Died;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    public void ApplyDamage(Rigidbody attachedBody, float force)
    {
        Animator.SetTrigger("fall");
        Vector3 direction = (transform.position - attachedBody.position).ToPlane();
        _body.AddForce(direction.normalized * force, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, 5f) == false)
        {
            _body.constraints = RigidbodyConstraints.None;
            Died?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enabled == false)
            return;
        
        if (other.TryGetComponent(out IDamageable damageable))
            damageable.ApplyDamage(_body, _body.velocity.magnitude);
    }
}
