using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject enemyPrefab;
    public List<Enemy> enemies;

    private void Awake() {
        instance = this; 
    }

    private void Start()
    {
        GameManager.instance.MaxScore = enemies.Count;
    }
    private void Update()
    {
        if (GameManager.instance.IsGameOver || GameManager.instance.IsWin)
            return;

        foreach (Enemy enemy in enemies)
        {
            if (!enemy.isSpawned && enemy.spawnerTime <= Time.timeSinceLevelLoad)
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
