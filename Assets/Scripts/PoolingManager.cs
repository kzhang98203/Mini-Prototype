using System.Collections.Generic;
using Egg_Scripts;
using UnityEngine;
using UnityEngine.Pool;

public class PoolingManager : MonoBehaviour
{
    //--------------------Singleton--------------------
    private static PoolingManager instance;

    public static PoolingManager Instance => instance;

    //-------------------------------------------------

    //--------------------Pooling--------------------
    public SpawnableController prefab;

    private readonly Dictionary<string, LinkedPool<SpawnableController>> pool =
        new Dictionary<string, LinkedPool<SpawnableController>>();
    //------------------------------------------------
        

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

        
    public SpawnableController GetFromPool(SpawnableController spawnable)
    {
        prefab = spawnable;
        if (pool.TryGetValue(spawnable.name, out LinkedPool<SpawnableController> spawnablePool))
        {
            return spawnablePool.Get();
        }
        else
        {
            return CreateNewObjectPool(spawnable);
        }
    }

    public void ReleaseToPool(SpawnableController spawnable)
    {
        if (pool.TryGetValue(spawnable.name, out LinkedPool<SpawnableController> spawnablePool))
        {
            if (spawnable.gameObject == null)
            {
                return;
            }
            if (spawnable.gameObject.activeSelf)
            {
                spawnablePool.Release(spawnable);
            }
                
        }
    }

    private SpawnableController CreateNewObjectPool(SpawnableController spawnable)
    {
        LinkedPool<SpawnableController> newBulletPool =
            new LinkedPool<SpawnableController>(CreateProjectile, OnGetProjectileFromPool, OnReturnProjectileToPool);
        pool.Add(spawnable.name, newBulletPool);
        return newBulletPool.Get();
    }

    private SpawnableController CreateProjectile()
    {
        var newSpawnable = Instantiate(prefab, transform);
        newSpawnable.gameObject.SetActive(false);
        newSpawnable.name = prefab.name;
        return newSpawnable;
    }

    private void OnGetProjectileFromPool(SpawnableController spawnable)
    {
            
    }

    private void OnReturnProjectileToPool(SpawnableController spawnable)
    {
        spawnable.gameObject.SetActive(false);
    }

    public void ClearAll()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.activeSelf)
            {
                var v = child.GetComponent<SpawnableController>();
                child.gameObject.SetActive(false);
                ReleaseToPool(v);
            }
        }
    }
}