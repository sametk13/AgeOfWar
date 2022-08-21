using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    public bool isEmpty = true;
    public Transform spawnPos;

    public void SpawnTurret(GameData gameData)
    {
        isEmpty = false;
        Instantiate(gameData.Prefab, spawnPos);
       
        Debug.Log("Spawn Turret");
    }
}
