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
        StartCoolDown(soldierData);
    }

    public void StartCoolDown(SoldierData soldierData)
    {
        float fillValue = 0f;

        DOTween.To(() => fillValue, x => fillValue = x, 1f, soldierData.SpawnDelayTime).SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                Debug.Log(fillValue);
                fillImage.fillAmount = fillValue;
            }).OnComplete(() =>
            {
                fillImage.fillAmount = 0;

                SpawnSoldier(soldierData);
            });
    }

    void SpawnSoldier(SoldierData soldierData)
    {
        OnSpawned?.Invoke();
        Instantiate(soldierData.Prefab, spawnTransform.position, Quaternion.identity, spawnTransform);
        Debug.Log(soldierData.name + " Spawned!!");
    }
}
