using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public SoldierData soldierData;

    public void SpawnItem()
    {
        SpawnManager.OnSpawn.Invoke(soldierData);
    }

}


