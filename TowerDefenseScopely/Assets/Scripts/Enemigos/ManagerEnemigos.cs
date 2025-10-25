using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemigos : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float SpawnInterval;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= SpawnInterval)
        {
            SpawnEnemy();
            timer = 0;  
        }
    }

    void SpawnEnemy()
    {
        if(enemyPrefabs.Length == 0 ||  spawnPoints.Length == 0)
        {
            return;
        }

        Enemy enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform transform = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
