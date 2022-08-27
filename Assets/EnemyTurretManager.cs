using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretManager : MonoBehaviour
{
    public void BuyTurret(ItemSlot itemSlot,TowerTurretData turretData)
    {
        itemSlot.SpawnTurret(turretData);
        EnemyGoldManager.Instance.DecreaseGold(turretData.Cost);
    }

    public void SellTurret(ItemSlot itemSlot)
    {
        TurretController turretController = itemSlot.GetComponentInChildren<TurretController>();
        EnemyGoldManager.Instance.IncreaseGold(turretController.turretData.EarnedMoneyAmountAfterSell);
        itemSlot.isEmpty = true;
        Destroy(turretController.gameObject);
    }
}
