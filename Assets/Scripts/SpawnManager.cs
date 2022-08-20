using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public static Action<GameData> OnSpawn;
    public static UnityEvent OnSpawned;
    [SerializeField] Transform spawnTransform;

    private void OnEnable()
    {
        OnSpawn += Spawn;
    }

    private void OnDisable()
    {
        OnSpawn -= Spawn;
    }

    public void Spawn(GameData gameData)
    {
        OnSpawned?.Invoke();
        Instantiate(gameData.Prefab, spawnTransform.position, Quaternion.identity, spawnTransform);
        Debug.Log(gameData.name + " Spawned!!");
    }
}
