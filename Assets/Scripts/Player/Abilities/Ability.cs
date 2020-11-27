using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : ScriptableObject
{
    protected Rigidbody Body;

    public virtual event UnityAction AbilityEnded;

    public void Init(Rigidbody _body)
    {
        Body = _body;
    }

    public abstract void UseAbility(AttackState player);
}
