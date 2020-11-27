using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Shuriken Ability", menuName = "Player/Abilities/Shuriken", order = 51)]
public class ShurikenAbility : Ability
{
    [SerializeField] private PlayerShuriken _shuriken;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState player)
    {
        InstatiateShuriken(player, player.transform.forward);
        InstatiateShuriken(player, player.transform.forward.RotateAroundY(5));
        InstatiateShuriken(player, player.transform.forward.RotateAroundY(-5));
        AbilityEnded?.Invoke();
    }

    private void InstatiateShuriken(AttackState player, Vector3 direction)
    {
        var inst = Instantiate(_shuriken, player.transform.position + Vector3.up + direction, Quaternion.identity);
        inst.SetDirection(direction);
    }
}
