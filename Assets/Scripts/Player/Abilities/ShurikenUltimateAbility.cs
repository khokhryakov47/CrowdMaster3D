using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Shuriken Ultimate Ability", menuName = "Player/Abilities/ShurikenUltimate", order = 51)]
public class ShurikenUltimateAbility : Ability
{
    [SerializeField] private PlayerShuriken _ultimateShuriken;
    [SerializeField] private PlayerShuriken _miniShuriken;

    private AttackState _attackState;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState attack)
    {
        _attackState = attack;
        _attackState.StartCoroutine(AttackCoroutine(attack));

        InstatiateShuriken(_ultimateShuriken, attack, attack.transform.forward);
        InstatiateShuriken(_ultimateShuriken, attack, attack.transform.forward.RotateAroundY(5));
        InstatiateShuriken(_ultimateShuriken, attack, attack.transform.forward.RotateAroundY(-5));
    }

    private IEnumerator AttackCoroutine(AttackState attackState)
    {
        int angle = 0;
        while (angle < 360)
        {
            InstatiateShuriken(_miniShuriken, attackState, attackState.transform.forward.RotateAroundY(angle));
            angle += 15;
            yield return new WaitForEndOfFrame();
        }
        AbilityEnded?.Invoke();
    }

    private void InstatiateShuriken(Shuriken shuriken, AttackState player, Vector3 direction)
    {
        var inst = Instantiate(shuriken, player.transform.position + Vector3.up, Quaternion.identity);
        inst.SetDirection(direction);
    }
}
