﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : PlayerState
{
    [SerializeField] private ManaAccumulator _manaAccumulator;

    private Ability _currentAbility;

    public event UnityAction<IDamageable> CollisionDetected;
    public event UnityAction AbilityEnded;

    private void OnEnable()
    {
        Animator.SetTrigger("attack");
        _currentAbility = _manaAccumulator.GetAbility();
        _currentAbility.UseAbility(this);

        _currentAbility.AbilityEnded += OnAbilityEnded;
    }

    private void OnDisable()
    {
        _currentAbility.AbilityEnded -= OnAbilityEnded;
    }

    private void OnAbilityEnded()
    {
        AbilityEnded?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
            CollisionDetected?.Invoke(damageable);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            CollisionDetected?.Invoke(damageable);
    }

    private void Update() { }
}