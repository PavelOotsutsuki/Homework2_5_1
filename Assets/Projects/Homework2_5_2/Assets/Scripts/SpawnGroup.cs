using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour
{
    private Spawn[] _spawns;

    private void Start()
    {
        _spawns = GetComponentsInChildren<Spawn>();
    }

    public bool TryGetRandomSpawnPosition(out float x, out float y)
    {
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
