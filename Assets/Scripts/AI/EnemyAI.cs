using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    EnemySpawn enemySpawn;
    EnemyGoldManager enemyGoldManager;
    EnemyTurretManager enemyTurretManager;
    [SerializeField] BaseController enemyBaseController;

    [SerializeField] SoldierData currentSoldierData;
    [SerializeField] TowerTurretData currentTurretData;

    bool isWaitTargetGold = false;
    int targetGold;

    bool isWaitSpawnEnemy = false;
    int needGold;

    bool isBuyEnemy = false;
    bool isBuyTurret = false;

    private void Awake()
    {
        enemySpawn = GetComponent<EnemySpawn>();
        enemyGoldManager = GetComponent<EnemyGoldManager>();
        enemyTurretManager = GetComponent<EnemyTurretManager>();
    }

    private void Start()
    {
        ChooseBuyItem();
    }

    private void OnEnable()
    {
        enemyGoldManager.OnGoldUpdated += SpawnEnemy;
        enemyGoldManager.OnGoldUpdated += SpawnTurret;
    }

    private void OnDisable()
    {
        enemyGoldManager.OnGoldUpdated -= SpawnEnemy;
        enemyGoldManager.OnGoldUpdated -= SpawnTurret;
    }

    void SpawnEnemy()
    {

        if (!isBuyEnemy) return;
        Debug.Log("Buying enemy");

        if (currentSoldierData == null)
        {
            currentSoldierData = StageEnemy.Instance.GetCurrentStage().SoldierDatas[UnityEngine.Random.Range(0, StageEnemy.Instance.GetCurrentStage().SoldierDatas.Count)];

        }
        if (!isWaitSpawnEnemy)
        {
            needGold = enemyGoldManager.Gold + currentSoldierData.Cost;
            isWaitSpawnEnemy = true;
        }

        if (enemyGoldManager.IsEnoughtGold(needGold) && enemySpawn.IsQueueEmtpty())
        {
            Debug.Log("Purchased enemy");

            enemyGoldManager.DecreaseGold(currentSoldierData.Cost);
            enemySpawn.StartSpawn(currentSoldierData);
            currentSoldierData = null;
            isWaitSpawnEnemy = false;
            ChooseBuyItem();
        }
    }

    void SpawnTurret()
    {
        
        if (!isBuyTurret) return;
        Debug.Log("Buying turret");
        if (currentTurretData == null)
        {
            currentTurretData = StageEnemy.Instance.GetCurrentStage().TowerTurrets[0];
        }

        ItemSlot targetItemSlot = enemyBaseController.GetTurretItemSlot();

        if (!targetItemSlot.isEmpty)
        {
            if (ChooseTurret(targetItemSlot.turretData) == null)
            {
                Debug.Log("Upgrade Tower !!!");
            }
            else
            {
                currentTurretData = ChooseTurret(targetItemSlot.turretData);
            }
        }


        if (enemyGoldManager.IsEnoughtGold(currentTurretData.Cost))
        {
            Debug.Log("Purchased turret");
            if(!targetItemSlot.isEmpty) enemyTurretManager.SellTurret(targetItemSlot);

            enemyTurretManager.BuyTurret(targetItemSlot, currentTurretData);
            isBuyTurret = false;
            currentTurretData = null;
            ChooseBuyItem();
            return;
        }
        else
        {
            if (!isWaitTargetGold)
            {
                targetGold = enemyGoldManager.Gold + 50;
                isWaitTargetGold = true;
            }


            if (enemyGoldManager.Gold >= targetGold)
            {
                isBuyTurret = false;
                isWaitTargetGold = false;
                ChooseBuyItem();
            }
        }
    }


    void UpgradeAge()
    {

    }


    void ChooseBuyItem()
    {
        int choosedItem = UnityEngine.Random.Range(0, 2);

        switch (choosedItem)
        {
            case 0:
                {
                    isBuyTurret = false;
                    isBuyEnemy = true;
                }
                break;
            case 1:
                {
                    isBuyTurret = true;
                    isBuyEnemy = false;
                }
                break;
        }
    }

    TowerTurretData ChooseTurret(TowerTurretData currentTurretData)
    {

        foreach (var data in StageEnemy.Instance.GetCurrentStage().TowerTurrets)
        {
            if(currentTurretData.Cost < data.Cost)
            {
                return data;
            }
        }
        return null;


        //foreach (var data in StageEnemy.Instance.GetCurrentStage().TowerTurrets)
        //{
        //    if (currentTurretData == null)
        //    {
        //        return data;
        //    }
        //    else if (currentTurretData.Cost < data.Cost)
        //    {
        //        return data;
        //    }
        //    else if (currentTurretData.Cost == StageEnemy.Instance.GetCurrentStage().TowerTurrets[StageEnemy.Instance.GetCurrentStage().TowerTurrets.Count - 1].Cost)
        //    {
        //        return StageEnemy.Instance.GetCurrentStage().TowerTurrets[StageEnemy.Instance.GetCurrentStage().TowerTurrets.Count - 1];
        //    }

        //}
        //return null;
    }
}
