using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private SpawnGroup _spawnGroup;

    private void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    private IEnumerator CreateEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(2);

        while (true)
        {
            float xPosition;
            float yPosition;

            if (_spawnGroup.TryGetRandomSpawnPosition(out xPosition, out yPosition)==false)
            {
                break;
            }
               
            Enemy createdEnemy = Instantiate(_enemy, new Vector3(xPosition, yPosition,0), Quaternion.identity);
            yield return wait;
        }
    }
}
