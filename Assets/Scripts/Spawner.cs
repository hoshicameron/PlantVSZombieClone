using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 0;
    [SerializeField] private float repeatRate = 0;
    [SerializeField] private GameObject[] enemyPrefabArray;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemies),spawnDelay,repeatRate);
    }

    private void SpawnEnemies()
    {
        GameObject enemy = PoolManager.Instance.ReuseObject(enemyPrefabArray[Random.Range(0, enemyPrefabArray.Length)],
            transform.position, Quaternion.identity);
        enemy.SetActive(true);
    }
}
