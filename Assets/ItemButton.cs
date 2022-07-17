using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public GameData gameData;

    public void SpawnItem()
    {
        SpawnManager.OnSpawn.Invoke(gameData);
    }

}


