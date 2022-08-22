using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static Action<SoldierData> OnSpawn;
    public static UnityEvent OnSpawned;
    [SerializeField] Transform spawnTransform;
    [SerializeField] Image fillImage;

    [SerializeField] Image currentSpawnedSoldier;

    [SerializeField] List<Image> spawnSoldierImageList = new List<Image>();
    [SerializeField] List<SoldierData> spawnSoldierQueue = new List<SoldierData>();
    [SerializeField] int instantMaxSpawnQueue = 7;

    bool isSpawnerWorking = false;
    private void Awake()
    {
        currentSpawnedSoldier.color = new Color(1, 1, 1, 0);
    }
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
        if (spawnSoldierQueue.Count < instantMaxSpawnQueue)
        {
            spawnSoldierQueue.Add(soldierData);
            SpawnedSoldierSpriteUpdater(spawnSoldierQueue[0]);
        }
        StartCoroutine(StartSpawnQueue());
    }

    IEnumerator StartSpawnQueue()
    {
        if (isSpawnerWorking == true) yield break;

        while (spawnSoldierQueue.Count > 0)
        {
            isSpawnerWorking = true;
            SoldierData soldierData = spawnSoldierQueue[0];

            bool isSpawn = false;
            float fillValue = 0f;

            SpawnedSoldierSpriteUpdater(soldierData);

            DOTween.To(() => fillValue, x => fillValue = x, 1f, soldierData.SpawnDelayTime).SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    Debug.Log(fillValue);
                    fillImage.fillAmount = fillValue;
                }).OnComplete(() =>
                {
                    fillImage.fillAmount = 0;

                    SpawnSoldier(soldierData);

                    currentSpawnedSoldier.color = new Color(1, 1, 1, 0);
                    spawnSoldierQueue.Remove(soldierData);
                    isSpawn = true;
                });
            yield return new WaitUntil(() => isSpawn == true);
        }
        isSpawnerWorking = false;
    }

    void SpawnSoldier(SoldierData soldierData)
    {
        OnSpawned?.Invoke();
        GameObject newSoldier = Instantiate(soldierData.Prefab, spawnTransform.position, Quaternion.identity, spawnTransform);
        SoldierManager.Instance.AddSoldierToList(newSoldier);
        Debug.Log(soldierData.name + " Spawned!!");
    }

    void SpawnedSoldierSpriteUpdater(SoldierData soldierData)
    {
        currentSpawnedSoldier.sprite = soldierData.sprite;
        currentSpawnedSoldier.color = new Color(1, 1, 1, 1);

        for (int i = 0; i < spawnSoldierImageList.Count; i++)
        {
            spawnSoldierImageList[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < spawnSoldierQueue.Count - 1; i++)
        {
            spawnSoldierImageList[i].color = new Color(1, 1, 1, 1);
            spawnSoldierImageList[i].sprite = spawnSoldierQueue[i + 1].sprite;
        }
    }
}
