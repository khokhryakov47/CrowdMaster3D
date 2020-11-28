using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Sword Ability", menuName = "Player/Abilities/Sword", order = 51)]
public class SwordAbility : Ability
{
    [SerializeField] private float _waveCount;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    [SerializeField] private float _waveDelay;

    private AttackState _player;
    private Coroutine _coroutine;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState player)
    {
        if (_coroutine != null)
            Reset();

        _player = player;
        _coroutine = player.StartCoroutine(AttackCoroutine(player, _radius));
    }

    private IEnumerator AttackCoroutine(AttackState player, float radius)
    {
        for (int i = 0; i < _waveCount; i++)
        {
            Collider[] colliders = Physics.OverlapSphere(player.transform.position + Vector3.up * 0.5f, radius);

            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IDamageable damageable))
                    damageable.ApplyDamage(player.Body, _damage);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(_waveDelay);
        }

        AbilityEnded?.Invoke();
        Reset();
    }

    private void Reset()
    {
        _player.StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
