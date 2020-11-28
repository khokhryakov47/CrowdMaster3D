using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerState _firstState;
    [SerializeField] private HealthContainer _health;

    private PlayerState _currentState;

    public event UnityAction Damaged;

    public Rigidbody Body { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        enabled = false;
        Animator.SetTrigger("broken");
    }

    private void Start()
    {
        _currentState = _firstState;
        _firstState.Enter(Body, Animator, _health);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(PlayerState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(Body, Animator, _health);
    }

    public bool ApplyDamage(Rigidbody body, float damage)
    {
        if (_currentState is PlayerDamageState == false || enabled == false)
            return false;

        _health.TakeDamage((int)damage);
        Damaged?.Invoke();
        return true;
    }
}