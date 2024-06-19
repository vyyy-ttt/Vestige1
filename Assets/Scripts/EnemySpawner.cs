using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 5;

    public int repeatCount = 5;
    public float interval = 15f;

    public float xMin = -6f;
    public float xMax = 5f;
    public float yMin = 3.75f;
    public float yMax = 4f;
    public float zMin = -1f;
    public float zMax = 10f;

    void Start()
    {
        if (!LevelManager.isGameOver)
        {
            //InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
            StartCoroutine(RepeatFunctionCoroutine(repeatCount, interval));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RepeatFunctionCoroutine(int count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(interval);
        }
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
