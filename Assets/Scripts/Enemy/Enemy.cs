using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private BrokenState _brokenState;
    [SerializeField] private HealthContainer _health;

    private EnemyState _currentState;
    private Rigidbody _body;

    public Animator Animator { get; private set; }
    public Player Player { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        Player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        _brokenState.Died += OnEnemyDied;
        _health.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _brokenState.Died -= OnEnemyDied;
        _health.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        enabled = false;
        _body.constraints = RigidbodyConstraints.None;
    }

    private void Start()
    {
        _currentState = _firstState;
        _firstState.Enter(Player, Animator);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(Player, Animator);
    }

    public bool ApplyDamage(Rigidbody body, float force)
    {   
        if (force > 5f && _currentState != _brokenState)
        {
            _health.TakeDamage((int)force);
            Transit(_brokenState);
            _brokenState.ApplyDamage(body, force);
            return true;
        }
        return false;
    }
}
