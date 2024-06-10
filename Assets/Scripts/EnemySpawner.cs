using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 5;

    public float xMin = -14f;
    public float xMax = 12f;
    public float yMin = 3.75f;
    public float yMax = 4.5f;
    public float zMin = -10f;
    public float zMax = 18f;

    void Start()
    {
        if (!LevelManager.isGameOver)
        {
            InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        Vector3 spawnPos;

        spawnPos.x = Random.Range(xMin, xMax);
        spawnPos.y = Random.Range(yMin, yMax);
        spawnPos.z = Random.Range(zMin, zMax);

        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPos, transform.rotation) as GameObject;

        spawnedEnemy.transform.parent = gameObject.transform;
    }
}
