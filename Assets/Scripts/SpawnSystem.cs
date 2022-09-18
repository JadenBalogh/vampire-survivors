using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private SpawnWave[] spawnWaves;
    [SerializeField] private float minSpawnY = -4f;
    [SerializeField] private float maxSpawnY = 4f;
    [SerializeField] private float minSpawnOffsetX = 12f;
    [SerializeField] private float maxSpawnOffsetX = 16f;

    private float currTime = 0f;

    private void Awake()
    {
        foreach (SpawnWave spawnWave in spawnWaves)
        {
            spawnWave.CanSpawn = true;
        }
    }

    private void Update()
    {
        foreach (SpawnWave spawnWave in spawnWaves)
        {
            if (currTime < spawnWave.startTime || currTime > spawnWave.endTime)
            {
                continue;
            }

            if (spawnWave.CanSpawn)
            {
                StartCoroutine(SpawnWaveCooldown(spawnWave));
                ApplySpawnWave(spawnWave);
            }
        }

        currTime += Time.deltaTime;
    }

    private void ApplySpawnWave(SpawnWave spawnWave)
    {
        int direction = Random.value > 0.5f ? 1 : -1;
        for (int i = 0; i < spawnWave.enemyCount; i++)
        {
            float spawnOffsetX = Random.Range(minSpawnOffsetX, maxSpawnOffsetX) * direction;
            float spawnY = Random.Range(minSpawnY, maxSpawnY);
            Vector2 spawnPos = new Vector2(GameManager.Player.transform.position.x + spawnOffsetX, spawnY);
            Instantiate(spawnWave.enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    private IEnumerator SpawnWaveCooldown(SpawnWave spawnWave)
    {
        spawnWave.CanSpawn = false;
        yield return new WaitForSeconds(Random.Range(spawnWave.minSpawnInterval, spawnWave.maxSpawnInterval));
        spawnWave.CanSpawn = true;
    }

    [System.Serializable]
    private class SpawnWave
    {
        public float startTime = 0f;
        public float endTime = 120f;
        public float minSpawnInterval = 10f;
        public float maxSpawnInterval = 15f;
        public int enemyCount = 20;
        public Enemy enemyPrefab;

        public bool CanSpawn { get; set; }
    }
}
