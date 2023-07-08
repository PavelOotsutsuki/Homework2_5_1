using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour
{
    private SpawnEnemy[] _spawns;

    private void Awake()
    {
        _spawns = GetComponentsInChildren<SpawnEnemy>();
    }

    public bool TryGetRandomSpawnPosition(out float x, out float y)
    {
        Debug.Log(_spawns.Length);

        if (_spawns.Length <= 0)
        {
            x = 0;
            y = 0;
            return false;
        }

        int spawnIndex = Random.Range(0, _spawns.Length);

        x = _spawns[spawnIndex].transform.position.x;
        y = _spawns[spawnIndex].transform.position.y;

        return true;
    }
}
