using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenDelayTransition : EnemyTransition
{
    [SerializeField] private float _delay;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(DelayCoroutine(_delay));
    }

    private IEnumerator DelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        NeedTransit = true;
    }
}
