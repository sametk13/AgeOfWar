using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : Damageable
{
    public BaseData baseData;
    ProgressBarController progressBar;
    public Transform turretRaycastStartPoint;

    GoldManager goldManager;

    ItemSlot worstTurret;

    [Header("Tower Settings")]
    [SerializeField] List<TowerSlot> turretSlots = new List<TowerSlot>();
    int turretTowerLevel = 1;

    private void Start()
    {
        goldManager = GetComponent<GoldManager>();
        progressBar = GetComponentInChildren<ProgressBarController>();
        Health = baseData.Health;
        MaxHealth = Health;
        progressBar.UpdateProgressBar(Health, MaxHealth);
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);
        progressBar.UpdateProgressBar(Health, MaxHealth);
    }

    public ItemSlot GetTurretItemSlot()
    {
        ItemSlot[] itemSlots = GetComponentsInChildren<ItemSlot>();
        foreach (var itemSlot in itemSlots)
        {
            if (itemSlot.isEmpty)
            {
                return itemSlot;
            }
        }
        foreach (var itemSlot in itemSlots)
        {
            if (worstTurret == null) worstTurret = itemSlot;

            if (itemSlot.turretData.Cost < worstTurret.turretData.Cost) worstTurret = itemSlot;
        }
        return worstTurret;
    }

    public void UpgradeTurretTower()
    {
        if (turretSlots.Count <= turretTowerLevel)
        {
            Debug.Log("Max Tower Level !!!");
        }
        else if (goldManager.IsEnoughtGold(turretSlots[turretTowerLevel].cost))
        {
            goldManager.DecreaseGold(turretSlots[turretTowerLevel].cost);
            turretTowerLevel++;
            OpenTurretSlot();          
        }
    }
    void OpenTurretSlot()
    {
        for (int i = 0; i < turretTowerLevel; i++)
        {
            turretSlots[i].turretSlot.SetActive(true);
        }
    }
}

[System.Serializable]
public class TowerSlot
{
    public GameObject turretSlot;
    public int cost;
}

