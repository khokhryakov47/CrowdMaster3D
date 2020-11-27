using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;
    public Player Player { get; private set; }
    public Animator Animator { get; private set; }
    public bool NeedTransit { get; protected set; }

    public void Init(Player player, Animator animator)
    {
        Player = player;
        Animator = animator;
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }
}
