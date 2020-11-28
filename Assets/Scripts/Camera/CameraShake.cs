using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.Damaged += OnPlayerDamaged;
    }

    private void OnDisable()
    {
        _player.Damaged -= OnPlayerDamaged;
    }

    private void OnPlayerDamaged()
    {
        _animator.SetTrigger("shake");
    }
}
