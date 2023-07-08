using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private Finish _finish;
    private Enemies _enemies;

    private void Start()
    {
        _finish = GetComponentInChildren<Finish>();
        _enemies = GetComponentInChildren<Enemies>();
    }

    private void Update()
    {
        if (_finish.IsSuccess)
        {
            _enemies.DestroyAllEnemies();
        }
    }
}
