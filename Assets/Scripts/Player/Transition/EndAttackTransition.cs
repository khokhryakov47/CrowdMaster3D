using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttackTransition : PlayerTransition
{
    [SerializeField] private AttackState _attack;

    protected override void Enable()
    {
        _attack.AbilityEnded += OnAbilityEnded;
    }

    private void OnDisable()
    {
        _attack.AbilityEnded -= OnAbilityEnded;
    }

    private void OnAbilityEnded()
    {
        NeedTransit = true;
    }

    private void Update() { }
}
