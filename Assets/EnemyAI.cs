using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    EnemySpawn enemySpawn;
    EnemyGoldManager enemyGoldManager;

    [SerializeField]SoldierData currentSoldierData;

    private void Awake()
    {
        enemySpawn = GetComponent<EnemySpawn>();
        enemyGoldManager = GetComponent<EnemyGoldManager>();
    }

    private void OnEnable()
    {
        enemyGoldManager.OnGoldUpdated += SpawnEnemy;
    }

    private void OnDisable()
    {
        enemyGoldManager.OnGoldUpdated -= SpawnEnemy;
    }

    void SpawnEnemy()
    {
        if (currentSoldierData == null)
        {
            currentSoldierData = StageEnemy.Instance.GetCurrentStage().SoldierDatas[UnityEngine.Random.Range(0, StageEnemy.Instance.GetCurrentStage().SoldierDatas.Count)];

        }
        if (enemyGoldManager.IsEnoughtGold(currentSoldierData.Cost) && enemySpawn.IsQueueEmtpty())
        {
            enemyGoldManager.DecreaseGold(currentSoldierData.Cost);
            enemySpawn.StartSpawn(currentSoldierData);
            currentSoldierData = null;
        }
    }

    void SpawnTurret()
    {

    }

    void UpgradeAge()
    {

    }
}
