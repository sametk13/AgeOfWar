using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public SoldierData soldierData;

    public void SpawnItem()
    {
        Debug.Log(SpawnManager.Instance.gameObject);
        SpawnManager.Instance.StartSpawn(soldierData);
    }

}


