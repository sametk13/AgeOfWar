using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : Damageable
{
    public BaseData baseData;
    ProgressBarController progressBar;
    public Transform turretRaycastStartPoint;

    ItemSlot worstTurret;

    private void Start()
    {
        progressBar = GetComponentInChildren<ProgressBarController>();
        Health = baseData.Health;
        MaxHealth = Health;
        progressBar.UpdateProgressBar(Health, MaxHealth);
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);
        progressBar.UpdateProgressBar(Health,MaxHealth);
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
}

