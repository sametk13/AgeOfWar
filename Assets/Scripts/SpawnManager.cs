using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public static Action<SoldierData> OnSpawn;
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

    public void Spawn(SoldierData soldierData)
    {
        OnSpawned?.Invoke();
        Instantiate(soldierData.Prefab, spawnTransform.position, Quaternion.identity,spawnTransform);
        Debug.Log(soldierData.name + " Spawned!!");
    }
}
