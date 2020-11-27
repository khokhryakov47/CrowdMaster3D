using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewer : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private HealthContainer _playerHealth;

    private void OnEnable()
    {
        _playerHealth.HealthChanged += OnPlayerHealthChanged;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(int health)
    {
        _text.text = health.ToString();
    }
}
