using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    [SerializeField] private List<PlayerTransition> _transitions;

    public Rigidbody Body { get; private set; }
    public Animator Animator { get; private set; }
    public HealthContainer Health { get; private set; }

    public void Enter(Rigidbody body, Animator animator, HealthContainer health)
    {
        if (enabled == false)
        {
            Body = body;
            Animator = animator;
            Health = health;
            enabled = true;
            foreach (var transition in _transitions)
            {
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

    public PlayerState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}
