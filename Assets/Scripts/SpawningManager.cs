using System;
using System.Collections;
using System.Collections.Generic;
using Egg_Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawningManager : MonoBehaviour
{
    [SerializeField] private SpawnableController acidRainPrefab;
    private float _lastSpawnTime;
    private void Start()
    {
        _lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _lastSpawnTime > 2)
        {
            _lastSpawnTime = Time.time;
            Spawn();
        }
    }

    private void Spawn()
    {
        var amount = Random.Range(1, 4);
        var startingY = 7;
        for (int i = 0; i < amount; i++)
        {
            var randomX = Random.Range(-2f, 2f);
            var spawnPoint = new Vector3(randomX, startingY, 0);

            var eggRoll = Random.Range(0f, 1f);

            SpawnableController spawnableController = PoolingManager.Instance.GetFromPool(acidRainPrefab);
            spawnableController.transform.rotation = Quaternion.identity;
            spawnableController.gameObject.SetActive(true);
            spawnableController.transform.position = spawnPoint;
            startingY += 2;
        }

        
    }
}
