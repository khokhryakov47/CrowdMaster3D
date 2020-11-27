using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Hand Ability", menuName = "Player/Abilities/Hand", order = 51)]
public class HandAbility : Ability
{
    [SerializeField] private float _utlimateTime;
    [SerializeField] private float _utlimateForce;

    private AttackState _ultimateState;
    private Coroutine _coroutine;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState ultimateState)
    {
        if (_coroutine != null)
            Reset();

        _ultimateState = ultimateState;

        _coroutine = ultimateState.StartCoroutine(AttackCoroutine(ultimateState));
        _ultimateState.CollisionDetected += OnPlayerAttack;
    }

    private void OnPlayerAttack(IDamageable damageable)
    {
        damageable.ApplyDamage(_ultimateState.Body, _utlimateForce);
        _ultimateState.Body.velocity = Vector3.zero;
    }

    private IEnumerator AttackCoroutine(AttackState ultimateState)
    {
        float time = _utlimateTime;
        while (time > 0)
        {
            ultimateState.Body.velocity = ultimateState.Body.velocity.normalized * _utlimateForce;
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
