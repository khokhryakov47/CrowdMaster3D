using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shuriken : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] private float _maxDistance;

    private Vector3 _startPosition;

    public Vector3 Direction { get; protected set; }
    public Rigidbody Body { get; private set; }
    
    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction.normalized;
    }

    private void Update()
    {
        Move();

        if (Vector3.Distance(_startPosition, transform.position) > _maxDistance)
            Destroy(gameObject);
    }

    protected abstract void Move();
    protected abstract void OnTriggerEnter(Collider collider);
}
