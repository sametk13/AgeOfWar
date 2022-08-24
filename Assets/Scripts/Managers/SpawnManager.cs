using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpawnManager : Spawn
{
    [SerializeField] Image currentSpawnedSoldier;
    [SerializeField] Image fillImage;
    [SerializeField] List<Image> spawnSoldierImageList = new List<Image>();

    public static Spawn Instance;


    private void Awake()
    {
        Instance = this;
        currentSpawnedSoldier.color = new Color(1, 1, 1, 0);
    }

    public override void SpawnSoldier(SoldierData soldierData)
    {
        OnSpawned?.Invoke();
        GameObject newSoldier = Instantiate(soldierData.Prefab, spawnTransform.position, Quaternion.Euler(new Vector3(0, 90, 0)), spawnTransform);

        SoldierController soldierController = newSoldier.GetComponent<SoldierController>();
        soldierController.soldierData = soldierData;
        soldierController._base = Base.base1;

        SoldierManager.Instance.AddSoldierToList(newSoldier);
        Debug.Log(soldierData.name + " Spawned!!");
    }

    public override void StartSpawn(SoldierData soldierData)
    {
        if (spawnSoldierQueue.Count < instantMaxSpawnQueue)
        {
            spawnSoldierQueue.Add(soldierData);
            SpawnedSoldierSpriteUpdater(spawnSoldierQueue[0]);
        }
        StartCoroutine(StartSpawnQueue());

    }

   public override IEnumerator StartSpawnQueue()
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
                    //Debug.Log(fillValue);
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
