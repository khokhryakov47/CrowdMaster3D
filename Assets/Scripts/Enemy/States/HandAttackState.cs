using System.Collections;
using UnityEngine;

public class HandAttackState : EnemyState
{
    [SerializeField] private float _attackForce;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(AttackCoroutine(2f));
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
    }

    private IEnumerator AttackCoroutine(float attackDelay)
    {
        while (enabled)
        {
            Animator.SetTrigger("attack");
            yield return new WaitForSeconds(attackDelay);
            Player.ApplyDamage(null, _attackForce);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }
}
