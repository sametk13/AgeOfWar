using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/TowerTurretData")]
public class TowerTurretData : GameData
{
    public float AttackRange;
    public float Damage;
    public float Cost;
    public float SellCost;
    public float AttackCooldown;
}
