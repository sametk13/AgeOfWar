using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Spawn : MonoBehaviour
{
    public List<SoldierData> spawnSoldierQueue;

    public static UnityEvent OnSpawned;

    public Transform spawnTransform;
    public int instantMaxSpawnQueue = 7;

    public bool isSpawnerWorking = false;



    private void Awake()
    {
        spawnSoldierQueue = new List<SoldierData>();
    }

    public virtual void StartSpawn(SoldierData soldierData )
    {
        if (IsQueueEmtpty())
        {

            spawnSoldierQueue.Add(soldierData);
        }
        StartCoroutine(StartSpawnQueue());
    }

    public virtual IEnumerator StartSpawnQueue()
    {
        if (isSpawnerWorking == true) yield break;

        while (spawnSoldierQueue.Count > 0)
        {

            isSpawnerWorking = true;
            SoldierData soldierData = spawnSoldierQueue[0];

            bool isSpawn = false;
            float fillValue = 0f;


            DOTween.To(() => fillValue, x => fillValue = x, 1f, soldierData.SpawnDelayTime).SetEase(Ease.Linear)
            .OnComplete(() =>
                {
                    SpawnSoldier(soldierData);
                    spawnSoldierQueue.Remove(soldierData);
                    isSpawn = true;
                });
            yield return new WaitUntil(() => isSpawn == true);
        }
        isSpawnerWorking = false;
    }

    public virtual void SpawnSoldier(SoldierData soldierData)
    {

        OnSpawned?.Invoke();
        GameObject newSoldier = Instantiate(soldierData.Prefab, spawnTransform.position, Quaternion.identity, spawnTransform);
        newSoldier.GetComponent<SoldierController>().soldierData = soldierData;
        SoldierManager.Instance.AddSoldierToList(newSoldier);
        Debug.Log(soldierData.name + " Spawned!!");
    }

    public bool IsQueueEmtpty()
    {
        if (spawnSoldierQueue.Count < instantMaxSpawnQueue)
        {

            return true;
        }
        else
        {

            return false;
        }
    }
}
