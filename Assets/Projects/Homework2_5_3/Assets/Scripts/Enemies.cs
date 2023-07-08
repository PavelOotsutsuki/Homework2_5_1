using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private EnemyPatrol[] _enemies;
    private int _destroyCounter;

    private void Start()
    {
        _enemies = GetComponentsInChildren<EnemyPatrol>();
        _destroyCounter = 0;
    }

    public void DestroyAllEnemies()
    {
        while (_destroyCounter < _enemies.Length)
        {
            Destroy(_enemies[_destroyCounter++].gameObject);
        }
    }
}
