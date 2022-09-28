using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Enemy> enemies;

    private void Update()
    {
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.isSpawned && enemy.spawnerTime <= Time.time)
            {
                if(enemy.randomSpawn) {
                    enemy.Spawner = Random.Range(0, transform.childCount);
                }
                GameObject enemyInstance = Instantiate(enemyPrefab, transform.GetChild(enemy.Spawner).transform);
                transform.GetChild(enemy.Spawner).GetComponent<SpawnPoint>().enemies.Add(enemyInstance);
                enemy.isSpawned = true;
            }
        }
    }
}
