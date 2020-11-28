using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenAttackState : EnemyState
{
    [SerializeField] private Transform _surikenContainer;
    [SerializeField] private EnemyShuriken _shurikenTemplate;
    [SerializeField] private float _minAttackDelay;
    [SerializeField] private float _maxAttackDelay;

    private void OnValidate()
    {
        if (_minAttackDelay > _maxAttackDelay)
            _minAttackDelay = _maxAttackDelay;
    }

    private void OnEnable()
    {
        StartCoroutine(AttackCoroutine(_minAttackDelay));
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
            var inst = Instantiate(_shurikenTemplate, _surikenContainer.position, Quaternion.identity);
            inst.SetDirection((Player.transform.position - transform.position).ToPlane());
            yield return new WaitForSeconds(Random.Range(_minAttackDelay, _maxAttackDelay));
        }
    }
}
