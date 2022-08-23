using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : Spawn
{
    public override void SpawnSoldier(SoldierData soldierData)
    {
        OnSpawned?.Invoke();
        GameObject newSoldier = Instantiate(soldierData.Prefab, spawnTransform.position, Quaternion.Euler(new Vector3(0,270,0)), spawnTransform);
        SoldierManager.Instance.AddSoldierToList(newSoldier);
        Debug.Log(soldierData.name + " Spawned!!");
    }
}
