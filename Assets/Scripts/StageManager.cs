using System;
using System.Collections;
using System.Collections.Generic;
using Egg_Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [SerializeField] private SpawnableController normalEggPrefab;
    [SerializeField] private SpawnableController goldenEggPrefab;
    [SerializeField] private SpawnableController hatchingEggPrefab;
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
            var randomX = Random.Range(-3f, 3f);
            //var randomY = Random.Range(7f, 10f);
            var spawnPoint = new Vector3(randomX, startingY, 0);

            var eggRoll = Random.Range(0f, 1f);
        
            SpawnableController eggController = eggRoll switch
            {
                < 0.05f => PoolingManager.Instance.GetFromPool(goldenEggPrefab),
                >= 0.05f and < 0.15f => PoolingManager.Instance.GetFromPool(hatchingEggPrefab),
                _ => PoolingManager.Instance.GetFromPool(normalEggPrefab)
            };
            eggController.gameObject.SetActive(true);
            eggController.transform.position = spawnPoint;
            startingY += 2;
        }

        
    }
}
