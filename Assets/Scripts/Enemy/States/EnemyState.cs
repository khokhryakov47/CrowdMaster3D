using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private List<EnemyTransition> _transitions;

    public Player Player { get; private set; }
    public Animator Animator { get; private set; }


    public void Enter(Player player, Animator animator)
    {
        if (enabled == false)
        {
            Player = player;
            Animator = animator;
            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.Init(player, animator);
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}
