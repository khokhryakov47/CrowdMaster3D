using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Hand Ability", menuName = "Player/Abilities/Hand", order = 51)]
public class HandAbility : Ability
{
    [SerializeField] private float _usefulTime;
    [SerializeField] private float _attackForce;

    private AttackState _ultimateState;
    private Coroutine _coroutine;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState attackState)
    {
        if (_coroutine != null)
            Reset();

        _ultimateState = attackState;

        _coroutine = attackState.StartCoroutine(AttackCoroutine(attackState));
        _ultimateState.CollisionDetected += OnPlayerAttack;
    }

    private void OnPlayerAttack(IDamageable damageable)
    {
        if (damageable.ApplyDamage(_ultimateState.Body, _attackForce) == false)
            return;

        _ultimateState.Body.velocity /= 2;
    }

    private IEnumerator AttackCoroutine(AttackState ultimateState)
    {
        float time = _usefulTime;
        while (time > 0)
        {
            ultimateState.Body.velocity = ultimateState.Body.velocity.normalized * _attackForce;
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Reset();
        AbilityEnded?.Invoke();
    }

    private void Reset()
    {
        _ultimateState.Body.velocity = Vector3.zero;
        _ultimateState.StopCoroutine(_coroutine);
        _coroutine = null;
        _ultimateState.CollisionDetected -= OnPlayerAttack;
    }
}
