using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : Spawn
{
    public override void SpawnSoldier(SoldierData soldierData)
    {
        OnSpawned?.Invoke();
        GameObject newSoldier = Instantiate(soldierData.Prefab, spawnTransform.position, Quaternion.Euler(new Vector3(0,270,0)), spawnTransform);
        SoldierController soldierController = newSoldier.GetComponent<SoldierController>();
        soldierController.soldierData = soldierData;
        soldierController._base = Base.base2;
        SoldierManager.Instance.AddSoldierToList(newSoldier);
        Debug.Log(soldierData.name + " Spawned!!");
    }
}
