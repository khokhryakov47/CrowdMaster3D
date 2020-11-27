using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerDamageState
{
    [SerializeField] private ManaAccumulator _manaAccumulator;
    [SerializeField] private PlayerControl _control;
    [SerializeField] private float _maxSpeed;

    public float MaxSpeed => _maxSpeed;

    private void OnEnable()
    {
        _control.DirectionChanged += OnDirectionChanged;
        _manaAccumulator.StartAccumulate();
    }

    private void OnDisable()
    {
        _control.DirectionChanged -= OnDirectionChanged;
        Animator.SetFloat("run", 0f);
    }

    private void OnDirectionChanged(Vector2 direction)
    {
        Body.velocity = new Vector3(direction.x, 0, direction.y) * 0.08f;
        if (Body.velocity.magnitude > _maxSpeed)
            Body.velocity *= _maxSpeed / Body.velocity.magnitude;

        if (Body.velocity.magnitude != 0)
            Body.rotation = Quaternion.LookRotation(Body.velocity, Vector3.up);
    }

    private void Update()
    {
        Animator.SetFloat("run", Body.velocity.magnitude / _maxSpeed);
    }
}