using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(Slider))]
public class EnemySlider : MonoBehaviour
{
    private List<Enemy> enemies;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        enemies = FindObjectsOfType<Enemy>().ToList();

        foreach (Enemy enemy in enemies)
        {
            enemy.Died += OnEnemyDied;
        }

        _slider.maxValue = enemies.Count;
        _slider.value = 0;
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Died -= OnEnemyDied;
        }
    }

    private void OnEnemyDied(Enemy diedEnemy)
    {
        enemies.Remove(diedEnemy);
        diedEnemy.Died -= OnEnemyDied;

        _slider.value++;
    }
}
