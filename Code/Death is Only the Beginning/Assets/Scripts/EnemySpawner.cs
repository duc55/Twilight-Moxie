using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Info")]
    [SerializeField] GameObject enemyToSpawnPrefab;
    [SerializeField] int totalEnemiesToSpawn;
    [SerializeField] float timeBetweenSpawns;
    float timeSinceLastSpawn;
    [SerializeField] float spawnRadius;

    void Update()
    {
        if (totalEnemiesToSpawn <= 0) return;

        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeBetweenSpawns) {
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        Vector3 offset = Random.insideUnitSphere;
        offset.y = 0f;
        GameObject enemy = Instantiate(enemyToSpawnPrefab, transform.position + offset, Quaternion.identity);
        totalEnemiesToSpawn--;
        timeSinceLastSpawn = 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
