using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    public TowerTurretData turretData;
    public bool isEmpty = true;
    public Transform spawnPos;

    public void SpawnTurret(TowerTurretData _turretData)
    {
        turretData = _turretData;
        isEmpty = false;
        GameObject turret = Instantiate(turretData.Prefab, spawnPos);
        turret.GetComponent<TurretController>().turretData = turretData;
        Debug.Log("Spawn Turret");
    }
}
