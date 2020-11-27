using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaAccumulator : MonoBehaviour
{
    [SerializeField] private float _accumulationTime;
    [SerializeField] private Ability _defaultAbility;
    [SerializeField] private Ability _maxAbility;

    private float _manaValue;

    public void StartAccumulate()
    {
        _manaValue = 0;
    }

    private void Update()
    {
        _manaValue += Time.deltaTime;
    }

    public Ability GetAbility()
    {
        if (_manaValue > _accumulationTime)
            return _maxAbility;

        return _defaultAbility;
    }
}
